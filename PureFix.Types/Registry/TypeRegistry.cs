using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Loader;
using System.Text.Json;
using PureFix.Types.Config;

namespace PureFix.Types.Registry;

/// <summary>
/// Thread-safe implementation of ITypeRegistry for managing multiple FIX type systems.
/// </summary>
public class TypeRegistry : ITypeRegistry
{
    private readonly ConcurrentDictionary<string, TypeRegistration> _registrations = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, ITypeSystemProvider> _providers = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, Assembly> _loadedAssemblies = new(StringComparer.OrdinalIgnoreCase);

    private string? _basePath;

    /// <summary>
    /// Load registrations from a JSON configuration file
    /// </summary>
    public void LoadFromFile(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"Type registry configuration file not found: {path}", path);

        _basePath = Path.GetDirectoryName(Path.GetFullPath(path));
        var json = File.ReadAllText(path);
        LoadFromJson(json);
    }

    /// <summary>
    /// Load registrations from JSON string
    /// </summary>
    public void LoadFromJson(string json)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };

        var config = JsonSerializer.Deserialize<TypeRegistryConfig>(json, options);
        if (config == null)
            throw new InvalidOperationException("Failed to deserialize type registry configuration");

        foreach (var registration in config.Registrations)
        {
            Register(registration);
        }
    }

    /// <summary>
    /// Register a type system programmatically
    /// </summary>
    public void Register(TypeRegistration registration)
    {
        if (string.IsNullOrWhiteSpace(registration.Name))
            throw new ArgumentException("Registration name is required", nameof(registration));

        _registrations[registration.Name] = registration;
    }

    /// <summary>
    /// Register a type system with its provider directly
    /// </summary>
    public void Register(TypeRegistration registration, ITypeSystemProvider provider)
    {
        Register(registration);
        _providers[registration.Name] = provider;
    }

    /// <summary>
    /// Get registration by name
    /// </summary>
    public TypeRegistration? GetByName(string name)
    {
        return _registrations.TryGetValue(name, out var registration) ? registration : null;
    }

    /// <summary>
    /// Find registration by SenderCompID (tag 49)
    /// </summary>
    public TypeRegistration? FindBySenderCompId(string senderCompId)
    {
        // First try exact matches (non-wildcard)
        var exactMatch = _registrations.Values
            .Where(r => r.Enabled && !r.SenderCompIds.Contains("*"))
            .FirstOrDefault(r => r.MatchesSenderCompId(senderCompId));

        if (exactMatch != null) return exactMatch;

        // Then try wildcard matches
        var wildcardMatch = _registrations.Values
            .Where(r => r.Enabled && r.SenderCompIds.Contains("*"))
            .FirstOrDefault(r => r.MatchesSenderCompId(senderCompId));

        if (wildcardMatch != null) return wildcardMatch;

        // Fall back to default
        return GetDefault();
    }

    /// <summary>
    /// Find registration by both SenderCompID and TargetCompID
    /// </summary>
    public TypeRegistration? FindByCompIds(string senderCompId, string targetCompId)
    {
        // First try exact matches
        var exactMatch = _registrations.Values
            .Where(r => r.Enabled)
            .Where(r => !r.SenderCompIds.Contains("*") && !r.TargetCompIds.Contains("*"))
            .FirstOrDefault(r => r.MatchesCompIds(senderCompId, targetCompId));

        if (exactMatch != null) return exactMatch;

        // Then try partial wildcard matches
        var partialMatch = _registrations.Values
            .Where(r => r.Enabled)
            .FirstOrDefault(r => r.MatchesCompIds(senderCompId, targetCompId));

        if (partialMatch != null) return partialMatch;

        return GetDefault();
    }

    /// <summary>
    /// Get the default registration
    /// </summary>
    public TypeRegistration? GetDefault()
    {
        return _registrations.Values.FirstOrDefault(r => r.IsDefault && r.Enabled);
    }

    /// <summary>
    /// Get all enabled registrations
    /// </summary>
    public IEnumerable<TypeRegistration> GetAllEnabled()
    {
        return _registrations.Values.Where(r => r.Enabled);
    }

    /// <summary>
    /// Get all registrations
    /// </summary>
    public IEnumerable<TypeRegistration> GetAll()
    {
        return _registrations.Values;
    }

    /// <summary>
    /// Get the type system provider for a registration
    /// </summary>
    public ITypeSystemProvider? GetProvider(string registrationName)
    {
        // Check if already loaded
        if (_providers.TryGetValue(registrationName, out var existingProvider))
            return existingProvider;

        // Get registration
        var registration = GetByName(registrationName);
        if (registration == null) return null;

        // Load the provider
        var provider = LoadProvider(registration);
        if (provider != null)
        {
            _providers[registrationName] = provider;
        }

        return provider;
    }

    /// <summary>
    /// Create a message factory for a registration
    /// </summary>
    public IFixMessageFactory? CreateMessageFactory(string registrationName)
    {
        var provider = GetProvider(registrationName);
        return provider?.CreateMessageFactory();
    }

    /// <summary>
    /// Create a session message factory for a registration
    /// </summary>
    public ISessionMessageFactory? CreateSessionMessageFactory(string registrationName, ISessionDescription sessionDescription)
    {
        var provider = GetProvider(registrationName);
        return provider?.CreateSessionMessageFactory(sessionDescription);
    }

    /// <summary>
    /// Check if a registration exists
    /// </summary>
    public bool Contains(string name)
    {
        return _registrations.ContainsKey(name);
    }

    /// <summary>
    /// Remove a registration
    /// </summary>
    public bool Remove(string name)
    {
        _providers.TryRemove(name, out _);
        return _registrations.TryRemove(name, out _);
    }

    /// <summary>
    /// Clear all registrations
    /// </summary>
    public void Clear()
    {
        _registrations.Clear();
        _providers.Clear();
        _loadedAssemblies.Clear();
    }

    private ITypeSystemProvider? LoadProvider(TypeRegistration registration)
    {
        // If no assembly path, try to find provider in already-loaded assemblies
        if (string.IsNullOrEmpty(registration.AssemblyPath))
        {
            return FindProviderInLoadedAssemblies(registration);
        }

        // Resolve assembly path
        var assemblyPath = ResolveAssemblyPath(registration.AssemblyPath);
        if (!File.Exists(assemblyPath))
        {
            throw new FileNotFoundException($"Type system assembly not found: {assemblyPath}", assemblyPath);
        }

        // Load assembly
        var assembly = LoadAssembly(assemblyPath);
        if (assembly == null) return null;

        // Find provider type
        var providerTypeName = registration.ProviderTypeName
            ?? $"{registration.RootNamespace}.TypeSystemProvider";

        var providerType = assembly.GetType(providerTypeName);
        if (providerType == null)
        {
            // Try searching for ITypeSystemProvider implementation
            providerType = assembly.GetTypes()
                .FirstOrDefault(t => typeof(ITypeSystemProvider).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
        }

        if (providerType == null)
        {
            throw new InvalidOperationException(
                $"Could not find type system provider '{providerTypeName}' in assembly '{assemblyPath}'");
        }

        // Create instance
        return Activator.CreateInstance(providerType) as ITypeSystemProvider;
    }

    private ITypeSystemProvider? FindProviderInLoadedAssemblies(TypeRegistration registration)
    {
        var providerTypeName = registration.ProviderTypeName
            ?? $"{registration.RootNamespace}.TypeSystemProvider";

        // Search in all loaded assemblies
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                var providerType = assembly.GetType(providerTypeName);
                if (providerType != null)
                {
                    return Activator.CreateInstance(providerType) as ITypeSystemProvider;
                }
            }
            catch
            {
                // Ignore assemblies that can't be searched
            }
        }

        return null;
    }

    private string ResolveAssemblyPath(string path)
    {
        if (Path.IsPathRooted(path))
            return path;

        if (_basePath != null)
            return Path.Combine(_basePath, path);

        return Path.GetFullPath(path);
    }

    private Assembly? LoadAssembly(string path)
    {
        if (_loadedAssemblies.TryGetValue(path, out var existing))
            return existing;

        try
        {
            // Use AssemblyLoadContext for proper dependency resolution
            var loadContext = AssemblyLoadContext.Default;
            var assembly = loadContext.LoadFromAssemblyPath(path);
            _loadedAssemblies[path] = assembly;
            return assembly;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to load assembly '{path}': {ex.Message}", ex);
        }
    }
}

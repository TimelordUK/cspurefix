using PureFix.Types;
using PureFix.Types.Config;
using PureFix.Types.Registry;

namespace PureFix.Test.ModularTypes.Registry;

[TestFixture]
public class TypeRegistryTests
{
    private TypeRegistry _registry = null!;

    [SetUp]
    public void Setup()
    {
        _registry = new TypeRegistry();
    }

    #region Registration Tests

    [Test]
    public void Register_SingleRegistration_CanBeRetrieved()
    {
        var registration = new TypeRegistration
        {
            Name = "fix44",
            DisplayName = "Standard FIX 4.4",
            Version = "FIX.4.4",
            RootNamespace = "PureFix.Types.FIX44",
            Enabled = true
        };

        _registry.Register(registration);

        var retrieved = _registry.GetByName("fix44");
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.DisplayName, Is.EqualTo("Standard FIX 4.4"));
    }

    [Test]
    public void Register_CaseInsensitiveLookup()
    {
        var registration = new TypeRegistration
        {
            Name = "FIX44",
            DisplayName = "Standard FIX 4.4",
            Enabled = true
        };

        _registry.Register(registration);

        Assert.That(_registry.GetByName("fix44"), Is.Not.Null);
        Assert.That(_registry.GetByName("FIX44"), Is.Not.Null);
        Assert.That(_registry.GetByName("Fix44"), Is.Not.Null);
    }

    [Test]
    public void Register_EmptyName_Throws()
    {
        var registration = new TypeRegistration { Name = "" };
        Assert.Throws<ArgumentException>(() => _registry.Register(registration));
    }

    [Test]
    public void Contains_ReturnsTrue_WhenRegistered()
    {
        _registry.Register(new TypeRegistration { Name = "test" });
        Assert.That(_registry.Contains("test"), Is.True);
    }

    [Test]
    public void Contains_ReturnsFalse_WhenNotRegistered()
    {
        Assert.That(_registry.Contains("nonexistent"), Is.False);
    }

    [Test]
    public void Remove_ReturnsTrue_WhenExists()
    {
        _registry.Register(new TypeRegistration { Name = "test" });
        Assert.That(_registry.Remove("test"), Is.True);
        Assert.That(_registry.Contains("test"), Is.False);
    }

    [Test]
    public void Remove_ReturnsFalse_WhenNotExists()
    {
        Assert.That(_registry.Remove("nonexistent"), Is.False);
    }

    [Test]
    public void Clear_RemovesAllRegistrations()
    {
        _registry.Register(new TypeRegistration { Name = "test1" });
        _registry.Register(new TypeRegistration { Name = "test2" });

        _registry.Clear();

        Assert.That(_registry.GetAll().Count(), Is.EqualTo(0));
    }

    #endregion

    #region SenderCompID Matching Tests

    [Test]
    public void FindBySenderCompId_ExactMatch()
    {
        _registry.Register(new TypeRegistration
        {
            Name = "jpmfx",
            SenderCompIds = new List<string> { "JPMORGAN", "JPM-FX" },
            Enabled = true
        });

        var result = _registry.FindBySenderCompId("JPMORGAN");
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo("jpmfx"));
    }

    [Test]
    public void FindBySenderCompId_CaseInsensitive()
    {
        _registry.Register(new TypeRegistration
        {
            Name = "jpmfx",
            SenderCompIds = new List<string> { "JPMORGAN" },
            Enabled = true
        });

        var result = _registry.FindBySenderCompId("jpmorgan");
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo("jpmfx"));
    }

    [Test]
    public void FindBySenderCompId_WildcardMatch()
    {
        _registry.Register(new TypeRegistration
        {
            Name = "default",
            SenderCompIds = new List<string> { "*" },
            Enabled = true
        });

        var result = _registry.FindBySenderCompId("ANYCLIENT");
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo("default"));
    }

    [Test]
    public void FindBySenderCompId_PrefixWildcard()
    {
        _registry.Register(new TypeRegistration
        {
            Name = "jpm",
            SenderCompIds = new List<string> { "JPM*" },
            Enabled = true
        });

        Assert.That(_registry.FindBySenderCompId("JPMORGAN")?.Name, Is.EqualTo("jpm"));
        Assert.That(_registry.FindBySenderCompId("JPM-FX")?.Name, Is.EqualTo("jpm"));
        Assert.That(_registry.FindBySenderCompId("BARCLAYS"), Is.Null);
    }

    [Test]
    public void FindBySenderCompId_ExactMatchPreferredOverWildcard()
    {
        _registry.Register(new TypeRegistration
        {
            Name = "jpmfx",
            SenderCompIds = new List<string> { "JPMORGAN" },
            Enabled = true
        });

        _registry.Register(new TypeRegistration
        {
            Name = "default",
            SenderCompIds = new List<string> { "*" },
            Enabled = true,
            IsDefault = true
        });

        var result = _registry.FindBySenderCompId("JPMORGAN");
        Assert.That(result!.Name, Is.EqualTo("jpmfx"));
    }

    [Test]
    public void FindBySenderCompId_FallsBackToDefault()
    {
        _registry.Register(new TypeRegistration
        {
            Name = "default",
            SenderCompIds = new List<string>(),
            Enabled = true,
            IsDefault = true
        });

        var result = _registry.FindBySenderCompId("UNKNOWN");
        Assert.That(result!.Name, Is.EqualTo("default"));
    }

    [Test]
    public void FindBySenderCompId_DisabledRegistrationIgnored()
    {
        _registry.Register(new TypeRegistration
        {
            Name = "disabled",
            SenderCompIds = new List<string> { "CLIENT" },
            Enabled = false
        });

        var result = _registry.FindBySenderCompId("CLIENT");
        Assert.That(result, Is.Null);
    }

    #endregion

    #region CompID Matching Tests

    [Test]
    public void FindByCompIds_MatchesBoth()
    {
        _registry.Register(new TypeRegistration
        {
            Name = "specific",
            SenderCompIds = new List<string> { "SENDER1" },
            TargetCompIds = new List<string> { "TARGET1" },
            Enabled = true
        });

        var result = _registry.FindByCompIds("SENDER1", "TARGET1");
        Assert.That(result!.Name, Is.EqualTo("specific"));
    }

    [Test]
    public void FindByCompIds_NoMatch_WhenOnlyOneSideMatches()
    {
        _registry.Register(new TypeRegistration
        {
            Name = "specific",
            SenderCompIds = new List<string> { "SENDER1" },
            TargetCompIds = new List<string> { "TARGET1" },
            Enabled = true
        });

        _registry.Register(new TypeRegistration
        {
            Name = "default",
            IsDefault = true,
            Enabled = true
        });

        var result = _registry.FindByCompIds("SENDER1", "WRONG_TARGET");
        Assert.That(result!.Name, Is.EqualTo("default"));
    }

    #endregion

    #region Default Registration Tests

    [Test]
    public void GetDefault_ReturnsDefaultRegistration()
    {
        _registry.Register(new TypeRegistration { Name = "other", Enabled = true });
        _registry.Register(new TypeRegistration { Name = "default", IsDefault = true, Enabled = true });

        var result = _registry.GetDefault();
        Assert.That(result!.Name, Is.EqualTo("default"));
    }

    [Test]
    public void GetDefault_ReturnsNull_WhenNoDefault()
    {
        _registry.Register(new TypeRegistration { Name = "other", Enabled = true });
        Assert.That(_registry.GetDefault(), Is.Null);
    }

    [Test]
    public void GetDefault_IgnoresDisabledDefault()
    {
        _registry.Register(new TypeRegistration { Name = "default", IsDefault = true, Enabled = false });
        Assert.That(_registry.GetDefault(), Is.Null);
    }

    #endregion

    #region GetAllEnabled Tests

    [Test]
    public void GetAllEnabled_FiltersDisabled()
    {
        _registry.Register(new TypeRegistration { Name = "enabled1", Enabled = true });
        _registry.Register(new TypeRegistration { Name = "disabled", Enabled = false });
        _registry.Register(new TypeRegistration { Name = "enabled2", Enabled = true });

        var enabled = _registry.GetAllEnabled().ToList();
        Assert.That(enabled.Count, Is.EqualTo(2));
        Assert.That(enabled.Any(r => r.Name == "disabled"), Is.False);
    }

    #endregion

    #region JSON Loading Tests

    [Test]
    public void LoadFromJson_ParsesCorrectly()
    {
        var json = @"{
            ""version"": ""1.0"",
            ""registrations"": [
                {
                    ""name"": ""fix44"",
                    ""displayName"": ""Standard FIX 4.4"",
                    ""version"": ""FIX.4.4"",
                    ""rootNamespace"": ""PureFix.Types.FIX44"",
                    ""senderCompIds"": [""*""],
                    ""enabled"": true,
                    ""isDefault"": true
                },
                {
                    ""name"": ""jpmfx"",
                    ""displayName"": ""JPMorgan FIX"",
                    ""version"": ""FIX.4.4"",
                    ""senderCompIds"": [""JPMORGAN"", ""JPM*""],
                    ""enabled"": true
                }
            ]
        }";

        _registry.LoadFromJson(json);

        Assert.That(_registry.GetAll().Count(), Is.EqualTo(2));
        Assert.That(_registry.GetByName("fix44")!.IsDefault, Is.True);
        Assert.That(_registry.GetByName("jpmfx")!.SenderCompIds.Count, Is.EqualTo(2));
    }

    [Test]
    public void LoadFromJson_SupportsComments()
    {
        var json = @"{
            // This is a comment
            ""version"": ""1.0"",
            ""registrations"": [
                {
                    ""name"": ""fix44"",
                    ""enabled"": true
                }
            ]
        }";

        Assert.DoesNotThrow(() => _registry.LoadFromJson(json));
        Assert.That(_registry.Contains("fix44"), Is.True);
    }

    [Test]
    public void LoadFromJson_SupportsTrailingCommas()
    {
        var json = @"{
            ""version"": ""1.0"",
            ""registrations"": [
                {
                    ""name"": ""fix44"",
                    ""enabled"": true,
                },
            ],
        }";

        Assert.DoesNotThrow(() => _registry.LoadFromJson(json));
    }

    #endregion

    #region Provider Tests

    [Test]
    public void Register_WithProvider_ProviderIsRetrievable()
    {
        var registration = new TypeRegistration
        {
            Name = "fix44",
            RootNamespace = "PureFix.Types.FIX44"
        };

        var provider = new MockTypeSystemProvider();
        _registry.Register(registration, provider);

        var retrieved = _registry.GetProvider("fix44");
        Assert.That(retrieved, Is.SameAs(provider));
    }

    [Test]
    public void CreateMessageFactory_WithProvider_CreatesFactory()
    {
        var registration = new TypeRegistration { Name = "test" };
        var provider = new MockTypeSystemProvider();
        _registry.Register(registration, provider);

        var factory = _registry.CreateMessageFactory("test");
        Assert.That(factory, Is.Not.Null);
    }

    [Test]
    public void CreateSessionMessageFactory_WithProvider_CreatesFactory()
    {
        var registration = new TypeRegistration { Name = "test" };
        var provider = new MockTypeSystemProvider();
        _registry.Register(registration, provider);

        var factory = _registry.CreateSessionMessageFactory("test", new MockSessionDescription());
        Assert.That(factory, Is.Not.Null);
    }

    #endregion

    #region TypeRegistration Pattern Matching Tests

    [Test]
    public void TypeRegistration_MatchesSenderCompId_ExactMatch()
    {
        var reg = new TypeRegistration
        {
            SenderCompIds = new List<string> { "CLIENT1", "CLIENT2" }
        };

        Assert.That(reg.MatchesSenderCompId("CLIENT1"), Is.True);
        Assert.That(reg.MatchesSenderCompId("CLIENT2"), Is.True);
        Assert.That(reg.MatchesSenderCompId("CLIENT3"), Is.False);
    }

    [Test]
    public void TypeRegistration_MatchesSenderCompId_Wildcard()
    {
        var reg = new TypeRegistration
        {
            SenderCompIds = new List<string> { "*" }
        };

        Assert.That(reg.MatchesSenderCompId("ANYTHING"), Is.True);
    }

    [Test]
    public void TypeRegistration_MatchesSenderCompId_PrefixWildcard()
    {
        var reg = new TypeRegistration
        {
            SenderCompIds = new List<string> { "JPM*" }
        };

        Assert.That(reg.MatchesSenderCompId("JPMORGAN"), Is.True);
        Assert.That(reg.MatchesSenderCompId("JPM-FX"), Is.True);
        Assert.That(reg.MatchesSenderCompId("BARCLAYS"), Is.False);
    }

    [Test]
    public void TypeRegistration_MatchesCompIds_BothMustMatch()
    {
        var reg = new TypeRegistration
        {
            SenderCompIds = new List<string> { "SENDER" },
            TargetCompIds = new List<string> { "TARGET" }
        };

        Assert.That(reg.MatchesCompIds("SENDER", "TARGET"), Is.True);
        Assert.That(reg.MatchesCompIds("SENDER", "WRONG"), Is.False);
        Assert.That(reg.MatchesCompIds("WRONG", "TARGET"), Is.False);
    }

    [Test]
    public void TypeRegistration_MatchesCompIds_EmptyListMatchesAny()
    {
        var reg = new TypeRegistration
        {
            SenderCompIds = new List<string> { "SENDER" },
            TargetCompIds = new List<string>() // Empty = match any
        };

        Assert.That(reg.MatchesCompIds("SENDER", "ANYTHING"), Is.True);
    }

    #endregion

    #region Mock Implementations

    private class MockTypeSystemProvider : ITypeSystemProvider
    {
        public string GetVersion() => "FIX.4.4";
        public string GetRootNamespace() => "Test";
        public IFixMessageFactory CreateMessageFactory() => new MockMessageFactory();
        public ISessionMessageFactory CreateSessionMessageFactory(ISessionDescription sd) => new MockSessionMessageFactory();
        public IEnumerable<Type> GetMessageTypes() => Array.Empty<Type>();
        public Type? GetMessageTypeByMsgType(string msgType) => null;
    }

    private class MockMessageFactory : IFixMessageFactory
    {
        public IFixMessage? ToFixMessage(IMessageView view) => null;
    }

    private class MockSessionMessageFactory : ISessionMessageFactory
    {
    }

    private class MockSessionDescription : ISessionDescription
    {
        public MsgApplication? Application => null;
        public string? Name => "test";
        public string? SenderCompID => "sender";
        public string? TargetCompID => "target";
        public bool? ResetSeqNumFlag => true;
        public int? MsgSeqNum => 1;
        public int? PeerSeqNum => 1;
        public string? SenderSubID => null;
        public string? TargetSubID => null;
        public string? BeginString => "FIX.4.4";
        public string? Username => null;
        public string? Password => null;
        public int? LastSentSeqNum => null;
        public int? LastReceivedSeqNum => null;
        public int? BodyLengthChars => 7;
        public int? HeartBtInt => 30;
    }

    #endregion
}

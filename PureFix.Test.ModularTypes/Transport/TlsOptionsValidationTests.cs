using PureFix.Types;
using PureFix.Types.Config;

namespace PureFix.Test.ModularTypes.Transport;

/// <summary>
/// A tls block written against another engine's spelling used to deserialise into nothing
/// and connect in plaintext without comment - the same class of defect as jspurefix#151,
/// where a declared option was silently dropped. Validate() now reports it.
/// </summary>
internal class TlsOptionsValidationTests
{
    private static TlsOptions Parse(string json)
    {
        var description = JsonHelper.FromJson<SessionDescription>(json);
        return description!.Application!.Tcp!.Tls!;
    }

    private const string JsPureFixSpelling = """
    {
      "application": {
        "tcp": {
          "host": "localhost",
          "port": 2344,
          "tls": {
            "timeout": 10000,
            "sessionTimeout": 10000,
            "key": "certs/client/client.key",
            "cert": "certs/client/client.crt",
            "ca": [ "certs/ca/ca.crt" ]
          }
        }
      }
    }
    """;

    [Test]
    public void Populated_block_without_enabled_is_reported_as_plaintext()
    {
        var tls = Parse(JsPureFixSpelling);
        var problems = tls.Validate();

        Assert.Multiple(() =>
        {
            Assert.That(tls.Enabled, Is.Null);
            Assert.That(problems.Any(p => p.Contains("PLAINTEXT")), Is.True,
                $"expected a plaintext warning, got: {string.Join(" / ", problems)}");
        });
    }

    [Test]
    public void Unrecognised_options_are_named_rather_than_dropped()
    {
        var tls = Parse(JsPureFixSpelling);
        var problems = tls.Validate();
        var unknown = problems.FirstOrDefault(p => p.Contains("unrecognised"));

        Assert.That(unknown, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(unknown, Does.Contain("key"));
            Assert.That(unknown, Does.Contain("cert"));
            Assert.That(unknown, Does.Contain("timeout"));
            // "ca" maps to a real property and must not be listed as unknown.
            Assert.That(tls.Ca, Is.Not.Null.And.Count.EqualTo(1));
        });
    }

    [Test]
    public void Certificate_verification_defaults_to_on()
    {
        Assert.That(new TlsOptions().ValidateServerCertificate, Is.True);
    }

    [Test]
    public void Disabling_verification_is_reported()
    {
        var tls = new TlsOptions { Enabled = true, ValidateServerCertificate = false };
        Assert.That(tls.Validate().Any(p => p.Contains("NOT verified")), Is.True);
    }

    [Test]
    public void A_coherent_block_reports_nothing()
    {
        var tls = new TlsOptions
        {
            Enabled = true,
            Certificate = "certs/client.pfx",
            TargetHost = "fix.broker.example.com",
            ValidateServerCertificate = true,
            Ca = ["certs/ca.crt"]
        };

        Assert.That(tls.Validate(), Is.Empty);
    }

    [Test]
    public void An_absent_block_is_not_flagged()
    {
        Assert.That(new TlsOptions().Validate(), Is.Empty);
    }
}

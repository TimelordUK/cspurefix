using PureFIix.Test.Env;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Ascii
{
    public class ElasticBufferTest
    {
        private FixDefinitions _definitions;

        [OneTimeSetUp]
        public void OnceSetup()
        {
            var rootFolder = Directory.GetCurrentDirectory();
            _definitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(_definitions);
            qf.Parse(Fix44PathHelper.DataDictPath);
        }

        [Test]
        public void One_char_in_buffer_length_1_Test()
        {
            var buffer = new ElasticBuffer(1);
            buffer.WriteChar(AsciiChars.Dot);
            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(1));
                Assert.That(buffer.ToString(), Is.EqualTo("."));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(1));
            });
        }

        [Test]
        public void String_in_buffer_Test()
        {
            var buffer = new ElasticBuffer(1);
            const string s = "fixing up fix";
            buffer.WriteString(s);

            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(s.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(s));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(16));
            });
        }

        [Test]
        public void Whole_Number_Test()
        {
            var buffer = new ElasticBuffer(10);
            const int n = 12345;
            buffer.WriteWholeNumber(n);
            var asString = buffer.ToString();

            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(n.ToString()));
                Assert.That(buffer.GetWholeNumber(0, asString.Length - 1), Is.EqualTo(n));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
            });

        }

        [Test]
        public void Whole_Number_Grow_Buffer_Test()
        {
            var buffer = new ElasticBuffer(1);
            const int n = 12345;
            buffer.WriteWholeNumber(n);
            var asString = buffer.ToString();

            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(n.ToString()));
                Assert.That(buffer.GetWholeNumber(0, asString.Length - 1), Is.EqualTo(n));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(8));
            });
        }

        [Test]
        public void Whole_Negative_Number_Test()
        {
            var buffer = new ElasticBuffer(10);
            const int n = -2468;
            buffer.WriteWholeNumber(n);
            var asString = buffer.ToString();


            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(n.ToString()));
                Assert.That(buffer.GetWholeNumber(0, asString.Length - 1), Is.EqualTo(n));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
            });
        }

        [Test]
        public void Get_Whole_Positive_Number_Test()
        {
            var buffer = new ElasticBuffer(10);
            const int n = 2468;
            buffer.WriteString($"+{n}");
            var asString = buffer.ToString();

            Assert.Multiple(() =>
            {
                Assert.That(buffer.ToString(), Is.EqualTo("+2468"));
                Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
                Assert.That(buffer.GetWholeNumber(0, asString.Length - 1), Is.EqualTo(n));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
            });
        }

        [Test]
        public void Floating_Point_Test()
        {
            var buffer = new ElasticBuffer(10);
            const double n = 12345.6789;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();

            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(n.ToString(CultureInfo.InvariantCulture)));
                var asf = buffer.GetFloat(0, asString.Length - 1);
                Assert.That(asf, Is.EqualTo(n));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
            });
        }

        [Test]
        public void Floating_Point_1dp_Test()
        {
            var buffer = new ElasticBuffer(10);
            const double n = 12345678.9;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();


            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(n.ToString(CultureInfo.InvariantCulture)));
                var asf = buffer.GetFloat(0, asString.Length - 1);
                Assert.That(asf, Is.EqualTo(n));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
            });
        }

        [Test]
        public void Negative_Floating_Point_Test()
        {
            var buffer = new ElasticBuffer(10);
            const double n = -12345.6789;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();

            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(n.ToString(CultureInfo.InvariantCulture)));
                var asf = buffer.GetFloat(0, asString.Length - 1);
                Assert.That(asf, Is.EqualTo(n));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(20));
            });
        }

        [Test]
        public void Floating_Point_Many_Dp_Test()
        {
            var buffer = new ElasticBuffer(10);
            const double n = 0.123456789;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();

            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(n.ToString(CultureInfo.InvariantCulture)));
                var asf = buffer.GetFloat(0, asString.Length - 1);
                Assert.That(asf, Is.EqualTo(n));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(20));
            });
        }

        [Test]
        public void Simple_Float_Point_1_Test()
        {
            var buffer = new ElasticBuffer(10);
            const double n = 3.9;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();


            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(n.ToString(CultureInfo.InvariantCulture)));
                var asf = buffer.GetFloat(0, asString.Length - 1);
                Assert.That(asf, Is.EqualTo(n));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
            });
        }

        [Test]
        public void Simple_Float_Point_2_Test()
        {
            var buffer = new ElasticBuffer(10);
            const double n = 35.77;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();

            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(n.ToString(CultureInfo.InvariantCulture)));
                var asf = buffer.GetFloat(0, asString.Length - 1);
                Assert.That(asf, Is.EqualTo(n));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
            });
        }

        [Test]
        public void Simple_Float_Point_3_Test()
        {
            var buffer = new ElasticBuffer(10);
            const double n = 0.058457;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();

            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(n.ToString(CultureInfo.InvariantCulture)));
                var asf = buffer.GetFloat(0, asString.Length - 1);
                Assert.That(asf, Is.EqualTo(n));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
            });
        }

        [Test]
        public void Simple_Float_Point_4_Test()
        {
            var buffer = new ElasticBuffer(10);
            const double n = -0.06445;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();
            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(n.ToString(CultureInfo.InvariantCulture)));
                var asf = buffer.GetFloat(0, asString.Length - 1);
                Assert.That(asf, Is.EqualTo(n));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
            });
        }

        [Test]
        public void Whole_Number_As_Float_Point()
        {
            var buffer = new ElasticBuffer(10);
            const int n = 123456789;
            buffer.WriteNumber(n);

            Assert.Multiple(() =>
            {
                var asString = buffer.ToString();
                Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(n.ToString()));
                var asf = buffer.GetFloat(0, asString.Length - 1);
                Assert.That(asf, Is.EqualTo(n));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(10));

            });
        }

        [Test]
        public void Tiny_Floating_Point()
        {
            var buffer = new ElasticBuffer(10);
            var n = 0.000000000001;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();
            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(((decimal)n).ToString(CultureInfo.InvariantCulture)));
                var asf = buffer.GetFloat(0, asString.Length - 1);
                Assert.That(asf, Is.EqualTo(n));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(20));
            });
        }

        [Test]
        public void Tiny_Negative_Floating_Point()
        {
            var buffer = new ElasticBuffer(10);
            const double n = -0.000000000001;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();
            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(((decimal)n).ToString(CultureInfo.InvariantCulture)));
                var asf = buffer.GetFloat(0, asString.Length - 1);
                Assert.That(asf, Is.EqualTo(n));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(20));

            });
        }

        [Test]
        public void Tiny_Float_Number_With_Sign_Test()
        {
            var buffer = new ElasticBuffer(10);
            var n = 0.000000000001;
            buffer.WriteString($"+{(decimal)n}");
            var asString = buffer.ToString();
            Assert.Multiple(() =>
            {
                Assert.That(buffer.ToString(), Is.EqualTo("+0.000000000001"));
                Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
                Assert.That(buffer.GetFloat(0, asString.Length - 1), Is.EqualTo(n));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(20));

            });
        }

        [Test]
        public void Boolean_True_Test()
        {
            var buffer = new ElasticBuffer(1);
            buffer.WriteBoolean(true);
            Assert.Multiple(() =>
            {
                Assert.That(buffer.ToString(), Is.EqualTo("Y"));
                Assert.That(buffer.GetPos(), Is.EqualTo(1));
                Assert.That(buffer.GetBoolean(0), Is.EqualTo(true));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(1));

            });
        }

        [Test]
        public void Boolean_False_Test()
        {
            var buffer = new ElasticBuffer(1);
            buffer.WriteBoolean(false);
            Assert.Multiple(() =>
            {
                Assert.That(buffer.ToString(), Is.EqualTo("N"));
                Assert.That(buffer.GetPos(), Is.EqualTo(1));
                Assert.That(buffer.GetBoolean(0), Is.EqualTo(false));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(1));
            }); 
        }

        [Test]
        public void Buffer_Write_Test() {
            var buffer = new ElasticBuffer(1);
            const string s = "fixing up fix";
            var b = Encoding.UTF8.GetBytes(s);
            buffer.WriteBuffer(b);

            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(b.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(s));
                var fetched = buffer.GetBuffer(0, b.Length - 1);
                Assert.That(fetched.ToArray(), Is.EqualTo(b));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(16));
            });
        }

        [Test]
        public void Buffer_Write_Chars_Test()
        {
            var buffer = new ElasticBuffer(1);
            const string s = "8=FIX.4.4";
            foreach(var c in s)
            {
                buffer.WriteChar((byte)c);
            }
            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(s.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(s));
            });
        }

        [Test]
        public void Buffer_Shrinks_Test()
        {
            var buffer = new ElasticBuffer(1);
            var s = new string('.', 60 * 1024);
            buffer.WriteString(s);
            Assert.Multiple(() =>
            {
                Assert.That(buffer.GetPos(), Is.EqualTo(s.Length));
                Assert.That(buffer.ToString(), Is.EqualTo(s));
                Assert.That(buffer.CurrentSize(), Is.EqualTo(65536));
                Assert.That(buffer.Reset(), Is.True);
                Assert.That(buffer.GetPos(), Is.EqualTo(0));
                Assert.That(buffer.CurrentSize, Is.LessThan(60 * 1024));
            });
        }

        [Test]
        public void Replace_Char_Test()
        {
            var buffer = new ElasticBuffer(1);
            const string s = "8=FIX.4.4";
            buffer.WriteString(s);
            Assert.Multiple(() =>
            {
                Assert.That(buffer.ToString(), Is.EqualTo(s));
                Assert.That(buffer.GetPos(), Is.EqualTo(s.Length));
                buffer.WriteChar(AsciiChars.Soh);
                Assert.That(buffer.GetPos(), Is.EqualTo(s.Length + 1));
                buffer.SwitchChar(AsciiChars.Pipe);
                Assert.That(buffer.GetPos(), Is.EqualTo(s.Length + 1));
                Assert.That(buffer.ToString(), Is.EqualTo($"{s}|"));
            });
        }

        [Test]
        public void Read_LocalTime_With_MS_Test()
        {
            const string ds = "10:39:01.621";
            var b = new ElasticBuffer(1);
            b.WriteString(ds);
            var dt = b.GetLocalTime(0, b.Pos - 1);
            Assert.That(dt, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(dt.Value.Hour, Is.EqualTo(10));
                Assert.That(dt.Value.Minute, Is.EqualTo(39));
                Assert.That(dt.Value.Second, Is.EqualTo(01));
                Assert.That(dt.Value.Millisecond, Is.EqualTo(621));
            });
        }

        [Test]
        public void Read_LocalTime_Without_MS_Test()
        {
            const string ds = "10:39:01";
            var b = new ElasticBuffer(1);
            b.WriteString(ds);
            var dt = b.GetLocalTime(0, b.Pos - 1);
            Assert.That(dt, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(dt.Value.Hour, Is.EqualTo(10));
                Assert.That(dt.Value.Minute, Is.EqualTo(39));
                Assert.That(dt.Value.Second, Is.EqualTo(01));
                Assert.That(dt.Value.Millisecond, Is.EqualTo(0));
            });
        }

        [Test]
        public void Write_UtcTime_Read_Utc_Test()
        {
            var utc = DateTime.Now.ToUniversalTime();
            var b = new ElasticBuffer(1);
            b.WriteUtcTimeOnly(utc);
            var dt = b.GetUtcTimeOnly(0, b.Pos - 1);
            Assert.That(dt, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(dt.Value.Hour, Is.EqualTo(utc.Hour));
                Assert.That(dt.Value.Minute, Is.EqualTo(utc.Minute));
                Assert.That(dt.Value.Second, Is.EqualTo(utc.Second));
                Assert.That(dt.Value.Millisecond, Is.EqualTo(utc.Millisecond));
            });
        }

        [Test]
        public void Write_Local_Read_Local_Test()
        {
            var local = DateTime.Now;
            var b = new ElasticBuffer(1);
            b.WriteLocalTimeOnly(local);
            var dt = b.GetLocalTime(0, b.Pos - 1);
            Assert.That(dt, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(dt.Value.Hour, Is.EqualTo(local.Hour));
                Assert.That(dt.Value.Minute, Is.EqualTo(local.Minute));
                Assert.That(dt.Value.Second, Is.EqualTo(local.Second));
                Assert.That(dt.Value.Millisecond, Is.EqualTo(local.Millisecond));
            });
        }
    }
}

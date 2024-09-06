using PureFIix.Test.Env;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Ascii
{
    public class ElasticBufferTest
    {
        private FixDefinitions _definitions;
        private SetConstraintHelper _setHelper;

        [OneTimeSetUp]
        public void OnceSetup()
        {
            var rootFolder = Directory.GetCurrentDirectory();
            _definitions = new FixDefinitions();
            _setHelper = new SetConstraintHelper(_definitions);
            var qf = new QuickFixXmlFileParser(_definitions);
            qf.Parse(Path.Join(rootFolder, "..", "..", "..", "..", "Data", "FIX44.xml"));
        }

        [Test]
        public void One_char_in_buffer_length_1_Test()
        {
            var buffer = new ElasticBuffer(1);
            buffer.WriteChar(AsciiChars.Dot);
            Assert.That(buffer.GetPos(), Is.EqualTo(1));
            Assert.That(buffer.ToString(), Is.EqualTo("."));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(1));
        }

        [Test]
        public void String_in_buffer_Test()
        {
            var buffer = new ElasticBuffer(1);
            var s = "fixing up fix";
            buffer.WriteString(s);
            Assert.That(buffer.GetPos(), Is.EqualTo(s.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(s));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(16));
        }

        [Test]
        public void Whole_Number_Test()
        {
            var buffer = new ElasticBuffer(10);
            var n = 12345;
            buffer.WriteWholeNumber(n);
            var asString = buffer.ToString();
            Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(n.ToString()));
            Assert.That(buffer.GetWholeNumber(0, asString.Length - 1), Is.EqualTo(n));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
        }

        [Test]
        public void Whole_Number_Grow_Buffer_Test()
        {
            var buffer = new ElasticBuffer(1);
            var n = 12345;
            buffer.WriteWholeNumber(n);
            var asString = buffer.ToString();
            Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(n.ToString()));
            Assert.That(buffer.GetWholeNumber(0, asString.Length - 1), Is.EqualTo(n));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(8));
        }

        [Test]
        public void Whole_Negative_Number_Test()
        {
            var buffer = new ElasticBuffer(10);
            var n = -2468;
            buffer.WriteWholeNumber(n);
            var asString = buffer.ToString();
            Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(n.ToString()));
            Assert.That(buffer.GetWholeNumber(0, asString.Length - 1), Is.EqualTo(n));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
        }

        [Test]
        public void Get_Whole_Positive_Number_Test()
        {
            var buffer = new ElasticBuffer(10);
            var n = 2468;
            buffer.WriteString($"+{n}");
            var asString = buffer.ToString();
            Assert.That(buffer.ToString(), Is.EqualTo("+2468"));
            Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
            Assert.That(buffer.GetWholeNumber(0, asString.Length - 1), Is.EqualTo(n));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
        }

        [Test]
        public void Floating_Point_Test()
        {
            var buffer = new ElasticBuffer(10);
            var n = 12345.6789;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();
            Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(n.ToString()));
            var asf = buffer.GetFloat(0, asString.Length - 1);
            Assert.That(asf, Is.EqualTo(n));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
        }

        [Test]
        public void Floating_Point_1dp_Test()
        {
            var buffer = new ElasticBuffer(10);
            var n = 12345678.9;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();
            Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(n.ToString()));
            var asf = buffer.GetFloat(0, asString.Length - 1);
            Assert.That(asf, Is.EqualTo(n));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
        }

        [Test]
        public void Negative_Floating_Point_Test()
        {
            var buffer = new ElasticBuffer(10);
            var n = -12345.6789;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();
            Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(n.ToString()));
            var asf = buffer.GetFloat(0, asString.Length - 1);
            Assert.That(asf, Is.EqualTo(n));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(20));
        }

        [Test]
        public void Floating_Point_Many_Dp_Test()
        {
            var buffer = new ElasticBuffer(10);
            var n = 0.123456789;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();
            Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(n.ToString()));
            var asf = buffer.GetFloat(0, asString.Length - 1);
            Assert.That(asf, Is.EqualTo(n));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(20));
        }

        [Test]
        public void Simple_Float_Point_1_Test()
        {
            var buffer = new ElasticBuffer(10);
            var n = 3.9;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();
            Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(n.ToString()));
            var asf = buffer.GetFloat(0, asString.Length - 1);
            Assert.That(asf, Is.EqualTo(n));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
        }

        [Test]
        public void Simple_Float_Point_2_Test()
        {
            var buffer = new ElasticBuffer(10);
            var n = 35.77;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();
            Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(n.ToString()));
            var asf = buffer.GetFloat(0, asString.Length - 1);
            Assert.That(asf, Is.EqualTo(n));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
        }

        [Test]
        public void Simple_Float_Point_3_Test()
        {
            var buffer = new ElasticBuffer(10);
            var n = 0.058457;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();
            Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(n.ToString()));
            var asf = buffer.GetFloat(0, asString.Length - 1);
            Assert.That(asf, Is.EqualTo(n));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
        }

        [Test]
        public void Simple_Float_Point_4_Test()
        {
            var buffer = new ElasticBuffer(10);
            var n = -0.06445;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();
            Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(n.ToString()));
            var asf = buffer.GetFloat(0, asString.Length - 1);
            Assert.That(asf, Is.EqualTo(n));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
        }

        [Test]
        public void Whole_Number_As_Float_Point()
        {
            var buffer = new ElasticBuffer(10);
            var n = 123456789;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();
            Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(n.ToString()));
            var asf = buffer.GetFloat(0, asString.Length - 1);
            Assert.That(asf, Is.EqualTo(n));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(10));
        }

        [Test]
        public void Tiny_Floating_Point()
        {
            var buffer = new ElasticBuffer(10);
            var n = 0.000000000001;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();
            Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(((decimal)n).ToString()));
            var asf = buffer.GetFloat(0, asString.Length - 1);
            Assert.That(asf, Is.EqualTo(n));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(20));
        }

        [Test]
        public void Tiny_Negative_Floating_Point()
        {
            var buffer = new ElasticBuffer(10);
            var n = -0.000000000001;
            buffer.WriteNumber(n);
            var asString = buffer.ToString();
            Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(((decimal)n).ToString()));
            var asf = buffer.GetFloat(0, asString.Length - 1);
            Assert.That(asf, Is.EqualTo(n));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(20));
        }


        [Test]
        public void Tiny_Float_Number_With_Sign_Test()
        {
            var buffer = new ElasticBuffer(10);
            var n = 0.000000000001;
            buffer.WriteString($"+{(decimal)n}");
            var asString = buffer.ToString();
            Assert.That(buffer.ToString(), Is.EqualTo("+0.000000000001"));
            Assert.That(buffer.GetPos(), Is.EqualTo(asString.Length));
            Assert.That(buffer.GetFloat(0, asString.Length - 1), Is.EqualTo(n));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(20));
        }

        [Test]
        public void Boolean_True_Test()
        {
            var buffer = new ElasticBuffer(1);
            buffer.WriteBoolean(true);
            var asString = buffer.ToString();
            Assert.That(buffer.ToString(), Is.EqualTo("Y"));
            Assert.That(buffer.GetPos(), Is.EqualTo(1));
            Assert.That(buffer.GetBoolean(0), Is.EqualTo(true));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(1));
        }

        [Test]
        public void Boolean_False_Test()
        {
            var buffer = new ElasticBuffer(1);
            buffer.WriteBoolean(false);
            var asString = buffer.ToString();
            Assert.That(buffer.ToString(), Is.EqualTo("N"));
            Assert.That(buffer.GetPos(), Is.EqualTo(1));
            Assert.That(buffer.GetBoolean(0), Is.EqualTo(false));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(1));
        }

        [Test]
        public void Buffer_Write_Test() {
            var buffer = new ElasticBuffer(1);
            var s = "fixing up fix";
            var b = Encoding.UTF8.GetBytes(s);
            buffer.WriteBuffer(b);
            Assert.That(buffer.GetPos(), Is.EqualTo(b.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(s));
            var fetched = buffer.GetBuffer(0, b.Length);
            Assert.That(fetched.ToArray(), Is.EqualTo(b));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(16));
        }

        [Test]
        public void Buffer_Write_Chars()
        {
            var buffer = new ElasticBuffer(1);
            var s = "8=FIX.4.4";
            foreach(var c in s)
            {
                buffer.WriteChar((byte)c);
            }
            Assert.That(buffer.GetPos(), Is.EqualTo(s.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(s));
        }

        [Test]
        public void Buffer_Shrinks()
        {
            var buffer = new ElasticBuffer(1);
            var s = new string('.', 60 * 1024);
            buffer.WriteString(s);
            Assert.That(buffer.GetPos(), Is.EqualTo(s.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(s));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(65536));
            buffer.Reset();
            Assert.That(buffer.GetPos(), Is.EqualTo(0));
        }

        /*
        test('buffer shrinks', () => {
                const buffer = new ElasticBuffer(1)
          const s = '.'.repeat(60 * 1024)
          buffer.writeString(s)
          expect(buffer.getPos()).toEqual(s.length)
          expect(buffer.toString()).toEqual(s)
          expect(buffer.currentSize()).toEqual(65536)
          expect(buffer.reset()).toBe(true)
          expect(buffer.getPos()).toEqual(0)
          expect(buffer.currentSize() < 60 * 1024).toBe(true)
        })
            }*/
    }
}

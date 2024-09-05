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
        public void One_char_in_buffer_length_1()
        {
            var buffer = new ElasticBuffer(1);
            buffer.WriteChar(AsciiChars.Dot);
            Assert.That(buffer.GetPos(), Is.EqualTo(1));
            Assert.That(buffer.ToString(), Is.EqualTo("."));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(1));
        }

        [Test]
        public void String_in_buffer()
        {
            var buffer = new ElasticBuffer(1);
            var s = "fixing up fix";
            buffer.WriteString(s);
            Assert.That(buffer.GetPos(), Is.EqualTo(s.Length));
            Assert.That(buffer.ToString(), Is.EqualTo(s));
            Assert.That(buffer.CurrentSize(), Is.EqualTo(16));
        }

        [Test]
        public void Whole_Number()
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
        public void Whole_Number_Grow_Buffer()
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
        public void Whole_Negative_Number()
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

/*
test('+ve number with explicit sign', () => {
        const n: number = 2468
  const buffer = new ElasticBuffer(10)
  buffer.writeString(`+${ n}`)
  const asString = buffer.toString()
  expect(asString).toEqual('+2468')
  expect(buffer.getWholeNumber(0, asString.length - 1)).toEqual(n)
})

test('floating point', () => {
        const n: number = 12345.6789
  const buffer = new ElasticBuffer(10)
  buffer.writeNumber(n)
  const asString = buffer.toString()
  expect(buffer.getPos()).toEqual(asString.length)
  expect(asString).toEqual('12345.6789')
  expect(buffer.getFloat(0, asString.length - 1)).toEqual(n)
  expect(buffer.currentSize()).toEqual(10)
})

test('floating point 1 dp', () => {
        const n: number = 12345678.9
  const buffer = new ElasticBuffer(10)
  buffer.writeNumber(n)
  const asString = buffer.toString()
  expect(buffer.getPos()).toEqual(asString.length)
  expect(asString).toEqual('12345678.9')
  expect(buffer.getFloat(0, asString.length - 1)).toEqual(n)
  expect(buffer.currentSize()).toEqual(10)
})

test('-ve floating point', () => {
        const n: number = -12345.6789
  const buffer = new ElasticBuffer(10)
  buffer.writeNumber(n)
  const asString = buffer.toString()
  expect(buffer.getPos()).toEqual(asString.length)
  expect(buffer.toString()).toEqual('-12345.6789')
  expect(buffer.getFloat(0, asString.length - 1)).toEqual(n)
  expect(buffer.currentSize()).toEqual(20)
})

test('floating point many dp', () => {
        const n: number = 0.123456789
  const buffer = new ElasticBuffer(10)
  buffer.writeNumber(n)
  const asString = buffer.toString()
  expect(buffer.getPos()).toEqual(asString.length)
  expect(asString).toEqual('0.123456789')
  expect(buffer.getFloat(0, asString.length - 1)).toEqual(n)
  expect(buffer.currentSize()).toEqual(20)
})

test('simple floating point 3.90', () => {
        const n: number = 3.90
  const buffer = new ElasticBuffer(10)
  buffer.writeNumber(n)
  const asString = buffer.toString()
  expect(buffer.getPos()).toEqual(asString.length)
  expect(asString).toEqual('3.9')
  expect(buffer.getFloat(0, asString.length - 1)).toEqual(n)
  expect(buffer.currentSize()).toEqual(10)
})

test('simple floating point 35.77', () => {
        const n: number = 35.77
  const buffer = new ElasticBuffer(10)
  buffer.writeNumber(n)
  const asString = buffer.toString()
  expect(buffer.getPos()).toEqual(asString.length)
  expect(asString).toEqual('35.77')
  expect(buffer.getFloat(0, asString.length - 1)).toEqual(n)
  expect(buffer.currentSize()).toEqual(10)
})

test('simple floating point 0.058457', () => {
        const n: number = 0.058457
  const buffer = new ElasticBuffer(10)
  buffer.writeNumber(n)
  const asString = buffer.toString()
  expect(buffer.getPos()).toEqual(asString.length)
  expect(asString).toEqual('0.058457')
  expect(buffer.getFloat(0, asString.length - 1)).toEqual(n)
  expect(buffer.currentSize()).toEqual(10)
})

test('simple floating point -0.06445', () => {
        const n: number = -0.06445
  const buffer = new ElasticBuffer(10)
  buffer.writeNumber(n)
  const asString = buffer.toString()
  expect(buffer.getPos()).toEqual(asString.length)
  expect(asString).toEqual('-0.06445')
  expect(buffer.getFloat(0, asString.length - 1)).toEqual(n)
  expect(buffer.currentSize()).toEqual(10)
})

test('whole number as floating point', () => {
        const n: number = 123456789
  const buffer = new ElasticBuffer(10)
  buffer.writeNumber(n)
  const asString = buffer.toString()
  expect(buffer.getPos()).toEqual(asString.length)
  expect(asString.toString()).toEqual('123456789')
  expect(buffer.getFloat(0, asString.length - 1)).toEqual(n)
  expect(buffer.currentSize()).toEqual(10)
})

test('tiny floating point', () => {
        const n: number = 0.000000000001
  const buffer = new ElasticBuffer(10)
  buffer.writeNumber(n)
  const asString = buffer.toString()
  expect(buffer.getPos()).toEqual(14)
  expect(asString).toEqual('0.000000000001')
  expect(buffer.getFloat(0, asString.length - 1)).toEqual(n)
  expect(buffer.currentSize()).toEqual(20)
})

test('tiny -ve floating point', () => {
        const n: number = -0.000000000001
  const buffer = new ElasticBuffer(10)
  buffer.writeNumber(n)
  const asString = buffer.toString()
  expect(buffer.getPos()).toEqual(asString.length)
  expect(asString).toEqual('-0.000000000001')
  expect(buffer.getFloat(0, asString.length - 1)).toEqual(n)
  expect(buffer.currentSize()).toEqual(20)
})

test('tiny +ve floating point with sign', () => {
        const n: number = 0.000000000001
  const buffer = new ElasticBuffer(10)
  buffer.writeString(`+${ n.toFixed(12)}`)
  const asString = buffer.toString()
  expect(buffer.getPos()).toEqual(asString.length)
  expect(asString).toEqual('+0.000000000001')
  expect(buffer.getFloat(0, asString.length - 1)).toEqual(n)
  expect(buffer.currentSize()).toEqual(20)
})

test('boolean true', () => {
        const buffer = new ElasticBuffer(1)
  buffer.writeBoolean(true)
  expect(buffer.getPos()).toEqual(1)
  expect(buffer.toString()).toEqual('Y')
  expect(buffer.currentSize()).toEqual(1)
  expect(buffer.getBoolean(0)).toBe(true)
})

test('boolean false', () => {
        const buffer = new ElasticBuffer(1)
  buffer.writeBoolean(false)
  expect(buffer.getPos()).toEqual(1)
  expect(buffer.toString()).toEqual('N')
  expect(buffer.currentSize()).toEqual(1)
  expect(buffer.getBoolean(0)).toBe(false)
})

test('write buffer', () => {
        const buffer = new ElasticBuffer(1)
  const s: string = 'fixing up fix'
  const b = Buffer.from(s)
  buffer.writeBuffer(b)
  expect(buffer.getPos()).toEqual(b.length)
  expect(buffer.toString()).toEqual(s)
  expect(buffer.currentSize() === 16)
  const fetched: Buffer = buffer.getBuffer(0, b.length)
  expect(fetched).toEqual(b)
})

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

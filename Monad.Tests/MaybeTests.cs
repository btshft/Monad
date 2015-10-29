using Monades.Maybe;
using NUnit.Framework;

namespace Monad.Tests
{
    [TestFixture]
    public class MaybeTests
    {
        [Test]
        public void TestEquals()
        {
            var m1 = Maybe.Of(12);
            var m2 = Maybe.Of(12);
            var m3 = Maybe.Of((int?) null);
            var m4 = Maybe.Of((int?) null);

            Assert.IsTrue(m1 == m2);
            Assert.IsTrue(m3 == m4);
        }

        [Test]
        public void TestQuery()
        {
            var m1 = 32.ToMaybe();
            var m2 = 54.ToMaybe();
            var m3 = ((int?) null).ToMaybe();

            var m4 = m1
                .FlatMap(m => m2)
                .FlatMap(m => m3);

            var m5 = m2.Bind((i => Maybe<int>.Some(i*2)));
            m3 = 32;

            var m6 = m3.Filter(val => val > 40);
            var m7 = m6.IfNone(() => 30);

            Assert.IsTrue(m4.IsNone);
            Assert.IsTrue(m6.IsNone);
            Assert.IsTrue(m7 == 30);

        }
    }
}
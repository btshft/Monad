using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Monad.MaybeMonad;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Monad.Tests
{
    [TestFixture]
    public class MaybeTests
    {
        [Test]
        public void TestEquals()
        {
            var m1 = Maybe.Create(12);
            var m2 = Maybe.Create(12);
            var m3 = Maybe.Create((int?) null);
            var m4 = Maybe.Create((int?) null);

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
                .Select(m => m2.Value)
                .Select(m => m3.GetValueOrDefault());

            var m5 = m2
                .Select(m => m1)
                .Where(m => m.Value > 50)
                .Select(m => m.GetValueOrDefault());

            Assert.IsFalse(m4.HasValue);
            Assert.IsFalse(m5.HasValue);


            var list = new List<Maybe<int>> {12, Maybe<int>.Nothing, 43, 345, Maybe<int>.Nothing, 23, Maybe<int>.Nothing };
            var filtered = list.Select(item => item).Flatten();
            Assert.IsTrue(filtered.Count() == list.Count - 3);



        }
    }
}

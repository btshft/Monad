using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
            var m1 = Maybe.MaybeOf(12);
            var m2 = Maybe.MaybeOf(12);
            var m3 = Maybe.MaybeOf((int?) null);
            var m4 = Maybe.MaybeOf((int?) null);

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
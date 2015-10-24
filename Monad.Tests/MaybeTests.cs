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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PayRoll.UnitTest.Helper;
using PayRoll.BLL;

namespace PayRoll.UnitTest.BLL
{
    [TestFixture]
    public class UnionAffiliationTest
    {
        [Test]
        public void NumberOfFridaysTest()
        {
            NUnitHelper nHelper = new NUnitHelper();
            UnionAffiliation ua = new UnionAffiliation(12, 34);

            int fridays = (int)nHelper.InvokePMethod(typeof(UnionAffiliation), "NumberOfFridays", ua, 
                new object[2]{new DateTime(2014,3,1), new DateTime(2014,3,31)});
            Assert.AreEqual(4, fridays);

            fridays = (int)nHelper.InvokePMethod(typeof(UnionAffiliation), "NumberOfFridays", ua,
                new object[2] { new DateTime(2014, 3, 1), new DateTime(2014, 4, 18) });
            Assert.AreEqual(7, fridays);

            fridays = (int)nHelper.InvokePMethod(typeof(UnionAffiliation), "NumberOfFridays", ua,
                new object[2] { new DateTime(2014, 4, 18), new DateTime(2014, 4, 30) });
            Assert.AreEqual(2, fridays);

        }
    }
}

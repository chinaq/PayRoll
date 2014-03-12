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
    public class BiWeeklyScheduleTest
    {
        [Test]
        public void IsEvenWeekTest_IsNotEven()
        { 
            NUnitHelper nHelper = new NUnitHelper();
            BiWeeklySchedule bs = new BiWeeklySchedule();
            bool isEven;
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 7) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 8) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 9) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 10) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 11) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 12) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 13) });
            Assert.IsFalse(isEven);

            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 21) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 22) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 23) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 24) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 25) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 26) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 27) });
            Assert.IsFalse(isEven);

            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(2017, 12, 25) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(2017, 12, 26) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(2017, 12, 27) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(2017, 12, 28) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(2017, 12, 29) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(2017, 12, 30) });
            Assert.IsFalse(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(2017, 12, 31) });
            Assert.IsFalse(isEven);
        }



        [Test]
        public void IsEvenWeekTest_IsEven()
        {
            NUnitHelper nHelper = new NUnitHelper();
            BiWeeklySchedule bs = new BiWeeklySchedule();
            bool isEven;
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 14) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 15) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 16) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 17) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 18) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 19) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 20) });
            Assert.IsTrue(isEven);

            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 28) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 29) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 30) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1987, 12, 31) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1988, 1, 1) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1988, 1, 2) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(1988, 1, 3) });
            Assert.IsTrue(isEven);

            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(2018, 1, 1) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(2018, 1, 2) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(2018, 1, 3) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(2018, 1, 4) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(2018, 1, 5) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(2018, 1, 6) });
            Assert.IsTrue(isEven);
            isEven = (bool)nHelper.InvokePMethod(typeof(BiWeeklySchedule), "IsEvenWeek", bs, new object[1] { new DateTime(2018, 1, 7) });
            Assert.IsTrue(isEven);
        }
    }
}

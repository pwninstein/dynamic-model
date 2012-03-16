using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DynamicModel.Test
{
    [TestClass]
    public class NotificationModelTest
    {
        private TestModel testModel;
        private NotificationModel<TestModel> notificationModel;
        private dynamic dynamicModel;

        [TestInitialize]
        public void TestInitialize()
        {
            testModel = new TestModel
            {
                Bool = false,
                Byte = byte.MinValue,
                Char = char.MinValue,
                DateTime = DateTime.MinValue,
                Decimal = decimal.MinValue,
                Double = double.MinValue,
                Float = float.MinValue,
                Int = int.MinValue,
                Long = long.MinValue,
                Short = short.MinValue,
                String = string.Empty
            };

            notificationModel = new NotificationModel<TestModel>(testModel);

            dynamicModel = notificationModel;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NotificationModelConstructorNullTest()
        {
            new NotificationModel<object>(null);
        }

        [TestMethod]
        public void RaisePropertyChangedTest()
        {
            bool propertyChangedRaised = false;

            notificationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                Assert.AreEqual(TestModel.IntProperty, e.PropertyName);
            };

            dynamicModel.Int = 5;

            Assert.IsTrue(propertyChangedRaised);
        }

        [TestMethod]
        public void ByteAssignmentTest()
        {
            dynamicModel.Byte = (byte)1;

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.ByteProperty]));

            Assert.AreEqual(testModel.Byte, (byte)1);
        }

        [TestMethod]
        public void ByteStringAssignmentTest()
        {
            dynamicModel.Byte = "1";

            Assert.AreEqual(testModel.Byte, (byte)1);

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.ByteProperty]));
        }

        [TestMethod]
        public void ShortAssignmentTest()
        {
            dynamicModel.Short = (short)2;

            Assert.AreEqual(testModel.Short, (short)2);

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.ShortProperty]));
        }

        [TestMethod]
        public void ShortStringAssignmentTest()
        {
            dynamicModel.Short = "2";

            Assert.AreEqual(testModel.Short, (short)2);

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.ShortProperty]));
        }

        [TestMethod]
        public void LongAssignmentTest()
        {
            dynamicModel.Long = (long)3;

            Assert.AreEqual(testModel.Long, (long)3);

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.LongProperty]));
        }

        [TestMethod]
        public void LongStringAssignmentTest()
        {
            dynamicModel.Long = "3";

            Assert.AreEqual(testModel.Long, (long)3);

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.LongProperty]));
        }

        [TestMethod]
        public void IntAssignmentTest()
        {
            dynamicModel.Int = 4;

            Assert.AreEqual(testModel.Int, (int)4);

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.IntProperty]));
        }

        [TestMethod]
        public void IntStringAssignmentTest()
        {
            dynamicModel.Int = "4";

            Assert.AreEqual(testModel.Int, (int)4);

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.IntProperty]));
        }

        [TestMethod]
        public void DoubleAssignmentTest()
        {
            dynamicModel.Double = 5D;

            Assert.AreEqual(testModel.Double, (double)5);

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.DoubleProperty]));
        }

        [TestMethod]
        public void DoubleStringAssignmentTest()
        {
            dynamicModel.Double = "5";

            Assert.AreEqual(testModel.Double, (double)5);

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.DoubleProperty]));
        }

        [TestMethod]
        public void FloatAssignmentTest()
        {
            dynamicModel.Float = 6F;

            Assert.AreEqual(testModel.Float, (float)6);

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.FloatProperty]));
        }

        [TestMethod]
        public void FloatStringAssignmentTest()
        {
            dynamicModel.Float = "6";

            Assert.AreEqual(testModel.Float, (float)6);

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.FloatProperty]));
        }

        [TestMethod]
        public void DecimalAssignmentTest()
        {
            dynamicModel.Decimal = 7M;

            Assert.AreEqual(testModel.Decimal, (decimal)7);

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.DecimalProperty]));
        }

        [TestMethod]
        public void DecimalStringAssignmentTest()
        {
            dynamicModel.Decimal = "7";

            Assert.AreEqual(testModel.Decimal, (decimal)7);

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.DecimalProperty]));
        }

        [TestMethod]
        public void BoolAssignmentTest()
        {
            dynamicModel.Bool = true;

            Assert.AreEqual(testModel.Bool, true);

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.BoolProperty]));
        }

        [TestMethod]
        public void BooleanStringAssignmentTest()
        {
            dynamicModel.Bool = "true";

            Assert.AreEqual(testModel.Bool, true);

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.BoolProperty]));
        }

        [TestMethod]
        public void CharAssignmentTest()
        {
            dynamicModel.Char = 'x';

            Assert.AreEqual(testModel.Char, 'x');

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.CharProperty]));
        }

        [TestMethod]
        public void StringAssignmentTest()
        {
            dynamicModel.String = "LOL";

            Assert.AreEqual(testModel.String, "LOL");

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.StringProperty]));
        }

        [TestMethod]
        public void DateTimeAssignmentTest()
        {
            var now = DateTime.Now;

            dynamicModel.DateTime = now;

            Assert.AreEqual(testModel.DateTime, now);

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.DateTimeProperty]));
        }

        [TestMethod]
        public void DateTimeStringAssignmentTest()
        {
            var now = DateTime.Now;

            dynamicModel.DateTime = now.ToString("O");

            Assert.AreEqual(testModel.DateTime, now);

            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.DateTimeProperty]));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AssignmentInvalidPropertyTest()
        {
            dynamicModel.ASDFQWER = 0;
        }

        [TestMethod]
        public void TryGetMemberTest()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void TrySetMemberTest()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ValidateTest()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ErrorTest()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ItemTest()
        {
            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void ModelTest()
        {
            Assert.AreEqual(testModel, notificationModel.Model);
        }
    }
}
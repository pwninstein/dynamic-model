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
        public void NotificationModelConstructorTest()
        {
            var notificationModel = new NotificationModel<TestModel>(testModel);

            Assert.AreEqual(testModel, notificationModel.Model);
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
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.ByteProperty]));
            Assert.AreEqual(testModel.Byte, (byte)1);
        }

        [TestMethod]
        public void ShortAssignmentTest()
        {
            dynamicModel.Short = (short)2;
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.ShortProperty]));
            Assert.AreEqual(testModel.Short, (short)2);
        }

        [TestMethod]
        public void ShortStringAssignmentTest()
        {
            dynamicModel.Short = "2";
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.ShortProperty]));
            Assert.AreEqual(testModel.Short, (short)2);
        }

        [TestMethod]
        public void LongAssignmentTest()
        {
            dynamicModel.Long = (long)3;
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.LongProperty]));
            Assert.AreEqual(testModel.Long, (long)3);
        }

        [TestMethod]
        public void LongStringAssignmentTest()
        {
            dynamicModel.Long = "3";
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.LongProperty]));
            Assert.AreEqual(testModel.Long, (long)3);
        }

        [TestMethod]
        public void IntAssignmentTest()
        {
            dynamicModel.Int = 4;
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.IntProperty]));
            Assert.AreEqual(testModel.Int, (int)4);
        }

        [TestMethod]
        public void IntStringAssignmentTest()
        {
            dynamicModel.Int = "4";
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.IntProperty]));
            Assert.AreEqual(testModel.Int, (int)4);
        }

        [TestMethod]
        public void DoubleAssignmentTest()
        {
            dynamicModel.Double = 5D;
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.DoubleProperty]));
            Assert.AreEqual(testModel.Double, (double)5);
        }

        [TestMethod]
        public void DoubleStringAssignmentTest()
        {
            dynamicModel.Double = "5";
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.DoubleProperty]));
            Assert.AreEqual(testModel.Double, (double)5);
        }

        [TestMethod]
        public void FloatAssignmentTest()
        {
            dynamicModel.Float = 6F;
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.FloatProperty]));
            Assert.AreEqual(testModel.Float, (float)6);
        }

        [TestMethod]
        public void FloatStringAssignmentTest()
        {
            dynamicModel.Float = "6";
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.FloatProperty]));
            Assert.AreEqual(testModel.Float, (float)6);
        }

        [TestMethod]
        public void DecimalAssignmentTest()
        {
            dynamicModel.Decimal = 7M;
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.DecimalProperty]));
            Assert.AreEqual(testModel.Decimal, (decimal)7);
        }

        [TestMethod]
        public void DecimalStringAssignmentTest()
        {
            dynamicModel.Decimal = "7";
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.DecimalProperty]));
            Assert.AreEqual(testModel.Decimal, (decimal)7);
        }

        [TestMethod]
        public void BoolAssignmentTest()
        {
            dynamicModel.Bool = true;
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.BoolProperty]));
            Assert.AreEqual(testModel.Bool, true);
        }

        [TestMethod]
        public void BooleanStringAssignmentTest()
        {
            dynamicModel.Bool = "true";
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.BoolProperty]));
            Assert.AreEqual(testModel.Bool, true);
        }

        [TestMethod]
        public void CharAssignmentTest()
        {
            dynamicModel.Char = 'x';
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.CharProperty]));
            Assert.AreEqual(testModel.Char, 'x');
        }

        [TestMethod]
        public void StringAssignmentTest()
        {
            dynamicModel.String = "LOL";
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.StringProperty]));
            Assert.AreEqual(testModel.String, "LOL");
        }

        [TestMethod]
        public void DateTimeAssignmentTest()
        {
            var now = DateTime.Now;

            dynamicModel.DateTime = now;
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.DateTimeProperty]));
            Assert.AreEqual(testModel.DateTime, now);
        }

        [TestMethod]
        public void DateTimeStringAssignmentTest()
        {
            var now = DateTime.Now;

            dynamicModel.DateTime = now.ToString("O");
            Assert.IsTrue(string.IsNullOrEmpty(notificationModel[TestModel.DateTimeProperty]));
            Assert.AreEqual(testModel.DateTime, now);
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
            Assert.Inconclusive("Not implemented");
        }
    }
}
//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================

namespace Domain.Seedwork.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Domain.Core;
    using Domain.Core.Tests.Classes;

    [TestClass()]
    public class EntityTests
    {
        
        [TestMethod()]
        public void DiferentIdProduceEqualsFalseTest()
        {
            //Arrange
            
            TestEntity entityLeft = new TestEntity();
            TestEntity entityRight = new TestEntity();

            entityLeft.Id = IdentityGenerator.SequentialGuid(); ;
            entityRight.Id = IdentityGenerator.SequentialGuid(); ;

            //Act
            bool resultOnEquals = entityLeft.Equals(entityRight);
            bool resultOnOperator = entityLeft == entityRight;

            //Assert
            Assert.IsFalse(resultOnEquals);
            Assert.IsFalse(resultOnOperator);

        }
        [TestMethod()]
        public void CompareUsingEqualsOperatorsAndNullOperandsTest()
        {
            //Arrange

            TestEntity entityLeft = null;
            TestEntity entityRight = new TestEntity();

            entityRight.Id = IdentityGenerator.SequentialGuid(); ;

            //Act
            if (!(entityLeft == (EntityBase)null))//this perform ==(left,right)
                Assert.Fail();

            if (!(entityRight != (EntityBase)null))//this perform !=(left,right)
                Assert.Fail();

            entityRight = null;

            //Act
            if (!(entityLeft == entityRight))//this perform ==(left,right)
                Assert.Fail();

            if (entityLeft != entityRight)//this perform !=(left,right)
                Assert.Fail();

          
        }
        [TestMethod()]
        public void CompareTheSameReferenceReturnTrueTest()
        {
            //Arrange
            TestEntity entityLeft = new TestEntity();
            TestEntity entityRight = entityLeft;


            //Act
            if (! entityLeft.Equals(entityRight))
                Assert.Fail();

            if (!(entityLeft == entityRight))
                Assert.Fail();

        }
        [TestMethod()]
        public void CompareWhenLeftIsNullAndRightIsNullReturnFalseTest()
        {
            //Arrange

            TestEntity entityLeft = null;
            TestEntity entityRight = new TestEntity();

            entityRight.Id = IdentityGenerator.SequentialGuid(); ;

            //Act
            if (!(entityLeft == (EntityBase)null))//this perform ==(left,right)
                Assert.Fail();

            if (!(entityRight != (EntityBase)null))//this perform !=(left,right)
                Assert.Fail();
        }
    }
}

using Com.Bateeq.Sales.Test.BussinesLogic.DataUtils.FinisihingPrintingSalesContract;
using Com.Bateeq.Sales.Test.BussinesLogic.Utils;
using Com.Bateeq.Service.Sales.Lib;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Facades.FinishingPrinting;
using Com.Bateeq.Service.Sales.Lib.BusinessLogic.Logic.FinishingPrinting;
using Com.Bateeq.Service.Sales.Lib.Models.FinishingPrinting;
using Com.Bateeq.Service.Sales.Lib.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Bateeq.Sales.Test.BussinesLogic.Facades.FinishingPrintingSalesContract
{
    public class FinishingPrintingSalesContractFacadeTest : BaseFacadeTest<SalesDbContext, FinishingPrintingSalesContractFacade, FinishingPrintingSalesContractLogic, FinishingPrintingSalesContractModel, FinisihingPrintingSalesContractDataUtil>
    {
        private const string ENTITY = "FinishingPrintingSalesContract";
        public FinishingPrintingSalesContractFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(SalesDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            var finishingprintingDetailLogic = new FinishingPrintingSalesContractDetailLogic(serviceProviderMock.Object, identityService, dbContext);
            var finishingprintingLogic = new FinishingPrintingSalesContractLogic(finishingprintingDetailLogic,serviceProviderMock.Object, identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(FinishingPrintingSalesContractLogic)))
                .Returns(finishingprintingLogic);

            return serviceProviderMock;
        }
    }
}

using System;
using Xunit;
using SoftwareManager.Shared;
using SoftwareManager.Server.Repositories;
using System.Linq;

namespace SoftwareManagerTests
{
    public class SoftwareManagerTests
    {
        private readonly ISoftwareManagerRepo _softwareManagerRepo;

        public SoftwareManagerTests()
        {
            _softwareManagerRepo = new SoftwareManagerRepo();
        }

        [Fact]
        public void ShouldReturnNullWithoutVersion()
        {
            var emptyResponse = _softwareManagerRepo.GetGreaterSoftware("");
            var nullResponse = _softwareManagerRepo.GetGreaterSoftware(null);

            Assert.Null(emptyResponse);
            Assert.Null(nullResponse);
        }

        [Theory]
        [InlineData("13.")]
        [InlineData("13.12.")]
        [InlineData("13.12.11.")]
        [InlineData("13.12.11.10")]
        [InlineData("13.12.11.10.9")]
        [InlineData("13.12.11.10.9.8.7.6")]
        [InlineData("13a")]
        [InlineData("13.12a")]
        [InlineData("13.12.11a")]
        public void ShouldReturnNullWithInvalidVersions(string versions)
        {
            Assert.Null(_softwareManagerRepo.GetGreaterSoftware(versions));
        }

        [Fact]
        public void ShouldReturnSoftwareWithGivenVersion()
        {
            const string version = "1";

            var response = _softwareManagerRepo.GetGreaterSoftware(version);

            Assert.NotNull(response);
        }

        [Fact]
        public void ShouldReturnSingletonSoftwareWithMaxVersion()
        {
            var software = new Software
            {
                Name = "Visual Studio",
                Version = "2019.1"
            };

            const string requestedVersion = "2019.0.1";

            var response = _softwareManagerRepo.GetGreaterSoftware(requestedVersion);

            Assert.NotNull(response);
            var softwareList = response.ToList();
            Assert.Single(softwareList);
            Assert.Equal(software.Name, softwareList.First().Name);
            Assert.Equal(software.Version, softwareList.First().Version);
        }
    }    
}

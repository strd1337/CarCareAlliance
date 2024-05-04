using CarCareAlliance.Presentation.Client.Models.Vehicles;
using Newtonsoft.Json.Linq;

namespace CarCareAlliance.Presentation.Client.Extensions
{
    public static class JObjectExtension
    {
        public static Vehicle? ToVehicle(this JObject jsonObject, string vin)
        {
            if (jsonObject.ContainsKey("status"))
            {
                return null;
            }

            var vehicle = new Vehicle
            {
                Brand = jsonObject["make"]["name"].ToString(),
                Model = jsonObject["model"]["name"].ToString(),
                Year = jsonObject["years"][0]["year"].ToObject<int>(),
                Vin = vin,
                Details = new VehicleDetails
                {
                    Engine = new VehicleEngineDetails
                    {
                        Name = jsonObject["engine"]["name"].ToString(),
                        Configuration = jsonObject["engine"]["configuration"].ToString(),
                        Horsepower = jsonObject["engine"]["horsepower"].ToString(),
                        Torque = jsonObject["engine"]["torque"].ToString(),
                        Cylinder = jsonObject["engine"]["cylinder"].ToString(),
                        Displacement = jsonObject["engine"]["displacement"].ToString(),
                        CompressorType = jsonObject["engine"]["compressorType"].ToString(),
                        Gear = jsonObject["engine"]["valve"]["gear"].ToString(),
                        CompressionRatio = jsonObject["engine"]["compressionRatio"].ToString(),
                        FuelType = jsonObject["engine"]["fuelType"].ToString()
                    },
                    Transmission = new VehicleTransmissionDetails
                    {
                        Name = jsonObject["transmission"]["name"].ToString(),
                        Type = jsonObject["transmission"]["transmissionType"].ToString(),
                        AutomaticType = jsonObject.ContainsKey("automaticType") ? jsonObject["transmission"]["automaticType"].ToString() : string.Empty,
                        NumberOfSpeeds = jsonObject["transmission"]["numberOfSpeeds"].ToString()
                    },
                    DrivenWheels = jsonObject["drivenWheels"].ToString(),
                }
            };

            return vehicle;
        }
    }
}

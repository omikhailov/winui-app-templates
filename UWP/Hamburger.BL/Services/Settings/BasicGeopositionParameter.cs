using Windows.Devices.Geolocation;
using Windows.Storage;
using Common.Services.Settings.Parameters;

namespace Hamburger.BL.Services.Settings
{
    public class BasicGeopositionParameter : Parameter<BasicGeoposition>
    {
        public BasicGeopositionParameter(string key, BasicGeoposition defaultValue, StorageType storageType) : base(key, defaultValue, storageType)
        {
                
        }

        public override BasicGeoposition Get()
        {
            if (Container.Values.TryGetValue(Key, out var savedValue))
            {
                var compositeValue = (ApplicationDataCompositeValue)savedValue;

                compositeValue.TryGetValue("Latitude", out var latitude);

                compositeValue.TryGetValue("Longitude", out var longitude);

                compositeValue.TryGetValue("Altitude", out var altitude);

                return new BasicGeoposition() { Latitude = (double)latitude, Longitude = (double)longitude, Altitude = (double)altitude };
            }
            else
            {
                return DefaultValue;
            }
        }

        public override void Set(BasicGeoposition value)
        {
            var compositeValue = new ApplicationDataCompositeValue();

            compositeValue.Add("Latitude", value.Latitude);

            compositeValue.Add("Longitude", value.Longitude);

            compositeValue.Add("Altitude", value.Altitude);

            Container.Values[Key] = compositeValue;

            OnChanged();
        }
    }
}

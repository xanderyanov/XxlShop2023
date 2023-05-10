using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace XxlStore
{
    [BsonIgnoreExtraElements]  //если какого-то поля нет - не выведет ошибку а проигнорирует
    public class Product : Father
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public ObjectId Id { get; set; }
        public string Code1C { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public List<string> CatLev { get; set; } = new();
        public string Article { get; set; }
        public string BrandName { get; set; }
        public string ImgFileName { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public double DiscountPrice { get; set; }
        public bool FlagNew { get; set; }
        public bool FlagSaleLeader { get; set; }
        public int TotalCount { get; set; }
        public int HM_Balance { get; set; }
        public int GL_Balance { get; set; }
        public int LZ_Balance { get; set; }
        public int OR_Balance { get; set; }
        public int PZ_Balance { get; set; }
        public int TA_Balance { get; set; }
        public int FO_Balance { get; set; }
        
        [BsonIgnore] public bool InStock => TotalCount > 0;
        [BsonIgnore] public bool OnSkladHM => HM_Balance > 0;
        [BsonIgnore] public bool OnSkladGL => GL_Balance > 0;
        [BsonIgnore] public bool OnSkladLZ => LZ_Balance > 0;
        [BsonIgnore] public bool OnSkladOR => OR_Balance > 0;
        [BsonIgnore] public bool OnSkladPZ => PZ_Balance > 0;
        [BsonIgnore] public bool OnSkladTA => TA_Balance > 0;
        [BsonIgnore] public bool ForOrder => FO_Balance > 0;


        public string Battery10 { get; set; }
        public string Bluetooth { get; set; }
        public string Barometer { get; set; }
        public string Wristlet { get; set; }
        public string Alarm { get; set; }
        public string ButtonsSoundToggler { get; set; }
        public string WaterProtection { get; set; }
        public string DirtResistance { get; set; }
        public string Extra { get; set; }
        public string TideIndicator { get; set; }
        public string BatteryLevelIndicator { get; set; }
        public string Compass { get; set; }
        public string CaseMaterial { get; set; }
        public string Melody { get; set; }
        public string WorldTime { get; set; }
        public string MoonData { get; set; }
        public string Gender { get; set; }
        public string ExactTimeRadioSignal { get; set; }
        public string CaseSize { get; set; }
        public string PowerReserve { get; set; }
        public string ExtraBrightBacklight { get; set; }
        public string SmartphoneConnection { get; set; }
        public string SolarBattery { get; set; }
        public string Glass { get; set; }
        public string BrandCountry { get; set; }
        public string ProducingCountry { get; set; }
        public string Taimer { get; set; }
        public string FishingTimer { get; set; }
        public string Thermometer { get; set; }
        public string MechanismType { get; set; }
        public string ImpactResistance { get; set; }
        public string MagneticFieldResistance { get; set; }
        public string CaseForm { get; set; }
        public string PhoneSearchFunction { get; set; }
        public string Stopwatch { get; set; }
        public string ClockhandMovement { get; set; }
        public string Chronograph { get; set; }
        public string ClockFace { get; set; }
        public string Pedometer { get; set; }


    }
}

namespace OptionsSample.Models
{
    public class MyOptionsWithDelegateConfig
    {
        public MyOptionsWithDelegateConfig()
        {
            Option1 = "value1_from_ctor";
        }

        public string Option1 { get; set; }

        public int Option2 { get; set; } = 5;
    }
}
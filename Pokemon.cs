using Newtonsoft.Json;

namespace Pokemon_JSON
{

    public class Pokemon
    {
        public string Name { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public Sprite Sprites { get; set; }
    }

    public class Sprite
    {
        [JsonProperty("front_default")]
        public string FrontDefault { get; set; }

        [JsonProperty("back_default")]
        public string BackDefault { get; set; }

        public Sprite()
        {
            FrontDefault = string.Empty;
            BackDefault = string.Empty;
        }
    }
}

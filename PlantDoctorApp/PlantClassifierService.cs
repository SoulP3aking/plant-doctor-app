using System;

namespace PlantDoctorApp
{
    public class PlantClassifierService
    {
        public Plant Classify(string imagePath)
        {
            // TODO: integrate TensorFlow model here
            // For now, return dummy data
            string name = "Unknown Plant";
            string condition = "Unknown";
            return new Plant(name, condition);
        }
    }
}

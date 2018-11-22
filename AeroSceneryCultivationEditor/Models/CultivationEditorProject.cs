using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.CultivationEditor.Models
{
    public class SceneryEditorProject
    {
        public SceneryEditorProject()
        {
            ProjectVersion = 1;
        }

        public int ProjectVersion { get; set; }
        public ProjectSettings ProjectSettings { get; set; }
        public Airport Airport { get; set; }

        public List<AirportDecal> AirportDecals { get; set; }
        public List<AirportGround> AirportGrounds { get; set; }
        public AirportTower AirportTower { get; set; }
        public List<AnimatedSceneryObject> AnimatedSceneryObjects { get; set; }
        public List<AutoCultivationArea> AutoCultivationAreas { get; set; }
        public List<AutoCultivationExclusionArea> AutoCultivationExclusionAreas { get; set; }
        public List<Building> Buildings { get; set; }
        public List<BuildingArea> BuildingAreas { get; set; }
        public List<Helipad> Helipads { get; set; }
        public List<Light> Lights { get; set; }
        public List<LightArea> LightAreas { get; set; }
        public List<LightLine> LightLines { get; set; }
        public List<ParkingPosition> ParkingPositions { get; set; }
        public List<Plant> Plants { get; set; }
        public List<PlantArea> PlantAreas { get; set; }
        public List<Runway> Runways { get; set; }
        public List<SceneryObject> SceneryObjects { get; set; }
        public List<StartPosition> StartPositions { get; set; }
        public List<ViewPosition> ViewPositions { get; set; }
    }
}

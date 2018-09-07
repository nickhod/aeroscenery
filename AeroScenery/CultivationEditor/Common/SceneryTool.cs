using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.SceneryEditor.Common
{
    public enum SceneryTool
    {
        Pointer,

        // Scenery
        Airport,
        AirportTower,
        AirportGround,
        AirportDecal,
        Runway,
        Helipad,
        Object,
        AnimatedOject,
        StartPosition,
        ParkingPosition,
        ViewPosition,

        // Cultivation
        AutoCultivationArea,
        AutoCultivationExclusionArea,
        BuildingAutoDetectionArea,
        BuildingArea,
        LightArea,
        LightLine,
        PlantArea,
        SingleBuilding,
        SingleLight,
        SinglePlant
    }
}

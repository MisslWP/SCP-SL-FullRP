using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanner
{
    public sealed class Config : IConfig
    {
        [Description("Состояние плагина (вкл/выкл)")]
        public bool IsEnabled { get; set; }

        [Description("Включены ли системы сканирования")]
        public bool enableScanning { get; private set; }

        [Description("Включены ли протоколы")]
        public bool enableProtocols { get; private set; }

        [Description("Включена ли маска для SCP-096")]
        public bool enable096Mask { get; private set; }

        

        [Description("Небольшие изменения (спавн 096 в своей камере)")]
        public bool enableSmallFeatures { get; private set; }

        [Description("Переработка SCP 106 (команды, начинающиеся с .106)")]
        public bool enable106overhaul { get; private set; }

        [Description("Сколько последних мест размещения портала будут считаться выходами из измерения")]
        public int additionalExitsCap { get; private set; } = 10;


        [Description("Включены ли случайные размеры людей при спавне")]
        public bool enableRandomSize { get; private set; }

        [Description("Минимальные и максимальные смещения размера тела по 3 координатам, требуется enable_random_size: true")]
        public float minXOffset { get; private set; } = 0.1f;
        public float maxXOffset { get; private set; } = 0.1f;
        public float minYOffset { get; private set; } = 0.1f;
        public float maxYOffset { get; private set; } = 0.1f;
        public float minZOffset { get; private set; } = 0.1f;
        public float maxZOffset { get; private set; } = 0.1f;

        [Description("Включены ли РП предметы (спец-патроны для SCP-173, транквилизатор)")]
        public bool enableRPItems { get; private set; }

        [Description("Добавляет новые предметы, появляющиеся на карте")]
        public bool enableNewItemSpawns { get; private set; }

        [Description("Добавляет команды, разработанные Дрёмой для FullRP")]
        public bool enableSuperCommands { get; private set; }

        [Description("Добавляет SCP-008")]
        public bool enable008 { get; private set; }
    }
}

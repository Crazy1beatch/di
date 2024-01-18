﻿using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        private readonly IDragonPainterFactory dragonPainterFactory;
        private readonly Func<Random, DragonSettingsGenerator> dragonSettingsGenerator;
        public DragonFractalAction(IDragonPainterFactory dragonPainterFactory, Func<Random, DragonSettingsGenerator> 
                dragonSettingsGenerator)
        {
            this.dragonPainterFactory = dragonPainterFactory;
            this.dragonSettingsGenerator = dragonSettingsGenerator;
        }
        
        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = CreateRandomSettings();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();
            // создаём painter с такими настройками
            dragonPainterFactory
                .CreateDragonPainter(dragonSettings)
                .Paint();

        }

        private DragonSettings CreateRandomSettings() => dragonSettingsGenerator(new Random()).Generate();
    }
}
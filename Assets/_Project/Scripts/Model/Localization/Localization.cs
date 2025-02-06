using System.Collections.Generic;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Model.Localization
{
    public class Localization
    {
        // TODO: должен быть метод, меняющий язык и обновляющий его для всех
        
        private Dictionary<LocalizationKeys, string> _localizedTexts;

        public Localization(string currentLanguage)
        {
            CurrentLanguage = new ReactiveProperty<string>(currentLanguage);

            LoadTranslation();
        }

        public ReactiveProperty<string> CurrentLanguage { get; private set; }
        
        public string GetTranslation(LocalizationKeys localizationKey)
        {
            return _localizedTexts[localizationKey];
        }

        private void LoadTranslation()
        {
            var resourceFile = Resources.Load<TextAsset>($"{Constants.LocalizationFolder}/{CurrentLanguage}");

            _localizedTexts = JsonConvert.DeserializeObject<Dictionary<LocalizationKeys, string>>(resourceFile.text);
        }
    }
}
using ChefRisingStar.Helpers;
using ChefRisingStar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ChefRisingStar.Services
{
    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class SubstitutionCache
    {
        private Dictionary<string, List<SubstituteIngredient[]>> _substitutions;
        private bool _isInitialized;

        public SubstitutionCache()
        {
            _substitutions = new Dictionary<string, List<SubstituteIngredient[]>>();
        }

        public bool Contains(string ingredient)
        {
            if (!_isInitialized)
            {
                LoadSubstitutionsFromFile();
            }

            ingredient = ingredient.ToLower();

            return _substitutions.ContainsKey(ingredient);
        }

        public void Add(string ingredient, List<SubstituteIngredient[]> substitutions)
        {
            if (!_isInitialized)
            {
                LoadSubstitutionsFromFile();
            }

            if (string.IsNullOrEmpty(ingredient) || substitutions == null || substitutions.Count == 0)
                return;

            ingredient = ingredient.ToLower();

            if (_substitutions.ContainsKey(ingredient))
                return;

            _substitutions.Add(ingredient, substitutions);
        }

        public ReadOnlyCollection<SubstituteIngredient[]> Get(string ingredient)
        {
            if (!_isInitialized)
            {
                LoadSubstitutionsFromFile();
            }

            ingredient = ingredient.ToLower();

            if (string.IsNullOrEmpty(ingredient) || !_substitutions.ContainsKey(ingredient))
                return null;

            return _substitutions[ingredient].AsReadOnly();
        }

        public void LoadSubstitutionsFromFile()
        {
            try
            {
                Substitution[] data = JsonFileLoader.GetCollection<Substitution>("Substitutions.json");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading ingredient cache: {ex}");
            }
            finally
            {
                _isInitialized = true;
            }
        }

        private string GetDebuggerDisplay()
        {
            return $"{_substitutions.Count} ingredients in cache";
        }

    }
}

using ClienteVip.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClienteVip.Wrapper
{
   public class ModelWrapper<T> : NotifyDataErrorInfoBase,
        IValidatableTrackingObject, IValidatableObject
    {

        private List<IValidatableTrackingObject> _trackingObjects;
        private Dictionary<string, object> _valoresOriginais;
        public ModelWrapper(T model) 
        {
            if (model == null)
                throw new ArgumentNullException("model");

            Model = model;
            _valoresOriginais = new Dictionary<string, object>();
            _trackingObjects = new List<IValidatableTrackingObject>();
            InicializarPropriedadeComplexas(model);
            InicializarColecoes(model);
            Validate();
        }

        protected virtual void InicializarColecoes(T model)
        {
            
        }

        protected virtual void InicializarPropriedadeComplexas(T model)
        {
           
        }

        public T Model { get; set; }
        public bool IsChanged { get { return _valoresOriginais.Count > 0 || _trackingObjects.Any(t=>t.IsChanged); } }

        public bool IsValid => !HasErrors && _trackingObjects.All(t => t.IsValid);

        protected TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            var propertyInfo = Model.GetType().GetProperty(propertyName);
            return (TValue)propertyInfo.GetValue(Model);
        }

        protected TValue GetOriginalValue<TValue>(string propertyName)
        {
            return _valoresOriginais.ContainsKey(propertyName)
              ? (TValue)_valoresOriginais[propertyName]
              : GetValue<TValue>(propertyName);
        }

        protected bool GetIsChanged(string propertyName)
        {
            return _valoresOriginais.ContainsKey(propertyName);
        }

        protected void SetValue<TValue>(TValue novoValor, [CallerMemberName] string propertyName = null)
        {
            var propertyInfo = Model.GetType().GetProperty(propertyName);
            var currentValue = propertyInfo.GetValue(Model);
            if (!Equals(currentValue, novoValor))
            {
                AtualizaValorOriginal(currentValue, novoValor, propertyName);
                propertyInfo.SetValue(Model, novoValor);
                OnPropertyChanged(propertyName);
                Validate();
                OnPropertyChanged(propertyName + "IsChanged");
            }
        }

        private void Validate()
        {
            ClearErrors();

            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);
            Validator.TryValidateObject(this, context, results, true);

            if (results.Any())
            {
                var propertyNames = results.SelectMany(r => r.MemberNames).Distinct().ToList();

                foreach (var propertyName in propertyNames)
                {
                    Errors[propertyName] = results
                      .Where(r => r.MemberNames.Contains(propertyName))
                      .Select(r => r.ErrorMessage)
                      .Distinct()
                      .ToList();
                    OnErrorsChanged(propertyName);
                }
            }
            OnPropertyChanged(nameof(IsValid));
        }

        protected void RegisterComplex<TModel>(ModelWrapper<TModel> wrapper)
        {
            RegisterTrackingObject(wrapper);
        }

        private void AtualizaValorOriginal(object currentValue, object novoValor, string propertyName)
        {
            if (!_valoresOriginais.ContainsKey(propertyName))
            {
                _valoresOriginais.Add(propertyName, currentValue);
                OnPropertyChanged("IsChanged");
            }
            else
            {
                if (Equals(_valoresOriginais[propertyName], novoValor))
                {
                    _valoresOriginais.Remove(propertyName);
                    OnPropertyChanged("IsChanged");
                }
            }
        }

        protected void RegisterCollection<TWrapper, TModel>(
            ChangeTrackingCollection<TWrapper> wrapperCollection,
            List<TModel> modelCollection) where TWrapper : ModelWrapper<TModel>
        {
            wrapperCollection.CollectionChanged += (s, e) =>
            {
                modelCollection.Clear();
                modelCollection.AddRange(wrapperCollection.Select(w => w.Model));
                Validate();
            };
            RegisterTrackingObject(wrapperCollection);
        }

        private void RegisterTrackingObject(IValidatableTrackingObject trackingObject)
        {
            if (!_trackingObjects.Contains(trackingObject))
            {
                _trackingObjects.Add(trackingObject);
                trackingObject.PropertyChanged += TrackingObjectPropertyChanged;
            }
        }

        private void TrackingObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsChanged))
            {
                OnPropertyChanged(nameof(IsChanged));
            }
            else if (e.PropertyName == nameof(IsValid))
            {
                OnPropertyChanged(nameof(IsValid));
            }
        }

        protected void RegistraColecoes<TWrapper, TModel>(ObservableCollection<TWrapper> wrapperColecao,
            List<TModel> modelColecao) where TWrapper : ModelWrapper<TModel>
        {
            wrapperColecao.CollectionChanged += (s, e) =>
            {
                if (e.OldItems != null)
                {
                    foreach (var item in e.OldItems.Cast<TWrapper>())
                    {
                        modelColecao.Remove(item.Model);
                    }
                }
                if (e.NewItems != null)
                {
                    foreach (var item in e.NewItems.Cast<TWrapper>())
                    {
                        modelColecao.Add(item.Model);
                    }
                }
            };
        }

        public void RejectChanges()
        {
            foreach (var originalValueEntry in _valoresOriginais)
            {
                typeof(T).GetProperty(originalValueEntry.Key).SetValue(Model, originalValueEntry.Value);
            }
            _valoresOriginais.Clear();
            foreach (var trackingObject in _trackingObjects)
            {
                trackingObject.RejectChanges();
            }
            OnPropertyChanged("");
        }

        public void AcceptChanges()
        {
            _valoresOriginais.Clear();
            foreach (var trackingObject in _trackingObjects)
            {
                trackingObject.AcceptChanges();
            }
            OnPropertyChanged("");
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}

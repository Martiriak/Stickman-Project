// using SerializeField = UnityEngine.SerializeField;

namespace Stickman.WeightWrapper
{
    /// <summary>
    /// Encapsulate an object and assigns it a specific weight,
    /// useful for random selection.
    /// 
    /// It's better if the weight is a positive value.
    /// </summary>
    /// <typeparam name="T">The wrapped object.</typeparam>
    [System.Serializable]
    public class WeightedObject<T>
    {
        public T Object;
        public float SpawnProbWeight;
        public int LevelSlotWeight = 1;
    }
}

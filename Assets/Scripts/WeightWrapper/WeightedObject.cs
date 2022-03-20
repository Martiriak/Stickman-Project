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
        public float Weight;


        // Non so se tornerà utile la roba scritta in seguito, conservarla male non fa.


        /*[SerializeField] private T m_wrappedObj;
        [SerializeField] private float m_weight;

        public T Object { get; private set; }
        public float Weight
        {
            get => m_weight;
            set
            {
                if (value < 0f) m_weight = -value;
                else m_weight = value;
            }
        }

        public ProbSelect(T obj, float weight)
        {
            Object = obj;
            Weight = weight;
        }*/
    }
}

using UnityEngine;

namespace Stickman.Props
{
    public interface IPropHandler
    {
        public void HandlePropFromObject(GameObject PropGameObject);

        /* DEFAULT IMPLEMENTATION
        
        public void HandlePropFromObject(GameObject PropGameObject)
        {
            PropCommand command = PropGameObject.GetComponent<Prop>()?.Consume();
            command?.Execute();
        }

        */
    }
}

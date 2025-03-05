using UnityEngine;

namespace LastBreakthrought.Infrustructure.Services
{
    public class MobileInput : InputService
    {
        public override Vector2 Axis =>
            GetSimpleInputAxis();
    }
}

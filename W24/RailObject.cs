namespace W24
{
    internal class RailObject
    {
        private string _name;
        private string _expectedState;
        private string _state;
        private bool _stateAsExpected;
        private bool _w24;

        public string Name { get => _name; }
        public string ExpectedState { get => _expectedState; }
        public string State { get => _state; set => _state = value; }
        public bool StateAsExpected { get => _stateAsExpected; set => _stateAsExpected = value; }
        public bool W24 { get => _w24; set => _w24 = value; }

        public RailObject(string name, string expectedState)
        {
            _name = name;
            _expectedState = expectedState;
            _stateAsExpected = false;
            _state = null;
            _w24 = false;
        }

        public void ChangeState(string response)
        {
            if (response != State)
            {
                if (response == "W24")
                {
                    W24 = true;
                }
                else
                {
                    State = response;

                    if (State == "S1")
                    {
                        W24 = false;
                    }
                }

                if (State == ExpectedState)
                {
                    StateAsExpected = true;
                }
                else
                {
                    StateAsExpected = false;
                }
            }
        }
    }
}

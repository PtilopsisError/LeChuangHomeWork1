public interface IState{
    abstract public void OnEnter();
    abstract public void OnUpdate();
    abstract public void OnFixedUpdate();
    abstract public void OnExit();
}
namespace Source.Scripts.Interfaces
{
    public interface IShootable
    {
        public bool CanShoot { get; set; }

        public void Shoot();
    }
}
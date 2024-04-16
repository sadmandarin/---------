namespace GridBoard
{
    public interface IBarracksTroopCell
    {
        public string Name { get; }
        public int Level { get; }
        public void Destroy();
    }
}

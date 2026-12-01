public class BonusPipeModel
    {
        public PipeType Type { get; private set; }
        public bool IsPlayerPlaced { get; private set; }


        public BonusPipeModel(PipeType type) 
        {
            Type = type;
            IsPlayerPlaced = false;
        }

         public void SetAsPlayerPlaced() => IsPlayerPlaced = true;


         public bool IsDirty() => Type == PipeType.DirtyStraightPipe ||
                                  Type == PipeType.DirtyCornerPipe;
      
    }


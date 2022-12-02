﻿namespace GSC_FinalProject.Entities
{
    public class Loan : EntityBase
    {
        public DateTime CreateDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public int PersonId { get; set; }
        public Person? Person { get; set; }
        public int ThingId { get; set; }
        public Thing? Thing { get; set; }
    }
}

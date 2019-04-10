using ResumeStripper.Models;

namespace ResumeStripper.DAL
{
    public class CvRepository : Repository<CV>
    {
        private readonly StripperContext _context;

        public CvRepository(StripperContext context) : base(context)
        {
            _context = context;
        }
    }
}
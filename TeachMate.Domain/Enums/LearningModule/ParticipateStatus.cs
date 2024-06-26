using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachMate.Domain
{
    public enum ParticipateStatus
    {
        Enrolled,       // Học viên đã ghi danh vào lớp học chưa đóng tiền, chưa học free
        PendingPayment,  // Học viên đã ghi danh nhưng chưa thanh toán
        HavePaid,       // Học viên đã thanh toán
       
        Completed,      // Học viên đã hoàn thành lớp học
        Dropped,        // Học viên đã rút khỏi lớp học
       
    }
}

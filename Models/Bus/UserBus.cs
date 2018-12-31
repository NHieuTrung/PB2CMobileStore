using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;

namespace Models.Bus
{
    public class UserBus
    {
        private MobileStoreContext db = null;
        public UserBus()
        {
            db= new MobileStoreContext();
        }

        public int Login(string userEmail,string passWord)
        {
            var result = db.Members.Count(x => x.MemberEmail == userEmail && x.MemberPassword == passWord);
            int? memberType = db.Members.Where(x => x.MemberEmail == userEmail && x.MemberPassword == passWord).Select(m => m.MemberTypeId).FirstOrDefault();
            if(result>0 && memberType==1)
            {
                return 1;
            }
            else if(result > 0 && memberType == 2)
            {
                return 2;
            }
            else if (result > 0 && memberType == 3)
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }

        public int InsertUserFb(Member member)
        {
            var modelMb = db.Members.SingleOrDefault(m => m.MemberEmail == member.MemberEmail || m.MemberFacebookId == member.MemberFacebookId);
            if (modelMb == null)
            {
                member.GenderTypeId = 4;
                member.RegDate = System.DateTime.Now;
                db.Members.Add(member);
                db.SaveChanges();
                return member.MemberId;
            }
            else
            {
                if (modelMb.MemberFacebookId == null)
                {
                    modelMb.MemberFacebookId = member.MemberFacebookId;
                    db.SaveChanges();
                    return modelMb.MemberId;
                }
                else if (modelMb.MemberEmail == null)
                {
                    modelMb.MemberEmail = member.MemberEmail;
                    db.SaveChanges();
                    return modelMb.MemberId;
                }
                return 0;
            }
        }

        public int InsertUserGg(Member member)
        {
            var modelMb = db.Members.Where(m => m.MemberEmail == member.MemberEmail || m.MemberGoogleId == member.MemberGoogleId).SingleOrDefault();
            if (modelMb == null)
            {
                member.GenderTypeId = 4;
                member.RegDate = System.DateTime.Now;
                db.Members.Add(member);

                db.SaveChanges();
                return member.MemberId;
            }
            else
            {
                if (modelMb.MemberGoogleId == null)
                {
                    modelMb.MemberGoogleId = member.MemberGoogleId;
                    db.SaveChanges();
                    return modelMb.MemberId;
                }
                else if (modelMb.MemberEmail == null)
                {
                    modelMb.MemberEmail = member.MemberEmail;
                    db.SaveChanges();
                    return modelMb.MemberId;
                }
                return 0;
            }
        }
    }
}

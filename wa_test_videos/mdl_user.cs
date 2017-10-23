using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wa_test_videos
{
    public class mdl_user
    {
        public static Guid str_fiduser;
        public static Guid code_user(string str_codeuser)
        {
            using (db_videos_testEntities data_user = new db_videos_testEntities())
            {
                var inf_user = (from i_u in data_user.inf_usuarios
                                where i_u.codigo_usuario == str_codeuser
                                select new
                                {
                                    i_u.id_usuario,

                                }).FirstOrDefault();

                Guid str_iduser = inf_user.id_usuario;
                return inf_user.id_usuario;
            }
        }
    }
}
namespace ChatTalk.WebServer.Data.Dapper.Sql
{
    public static class UsersSql
    {
        public static readonly string SELET_LIST_001 = """
            SELECT ID        AS Id
                 , CREATE_AT AS CreateAt
                 , USER_ID   AS UserId
                 , USER_NAME AS UserName
              FROM USERS;
            """;

        public static readonly string SELECT_ONE_001 = """
            SELECT USER_ID   AS UserId
                 , USER_NAME AS UserName
              FROM USERS
             WHERE 1=1
               AND USER_ID = @UserId;
            """;
    }
}
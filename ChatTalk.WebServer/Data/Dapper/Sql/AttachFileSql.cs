namespace ChatTalk.WebServer.Data.Dapper.Sql
{
    public static class AttachFileSql
    {
        public static readonly string SELECT_ONE_001 = """
            SELECT FILE_ID      AS FileId
                , REF_TYPE      AS RefType
                , REF_ID        AS RefId
                , ORIGINAL_NAME AS OriginalName
                , STORED_NAME   AS StoredName
                , FILE_PATH     AS FilePath
                , FILE_SIZE     AS FileSize
                , CONTENT_TYPE  AS ContentType
                , REMARK        AS Remark
                , CREATED_BY    AS CreatedBy
                , CREATED_AT    AS CreateAt
             FROM ATTACH_FILE
            WHERE 1=1
              AND REF_TYPE = @RefType
              AND REF_ID = @RefId
            """;
    }
}

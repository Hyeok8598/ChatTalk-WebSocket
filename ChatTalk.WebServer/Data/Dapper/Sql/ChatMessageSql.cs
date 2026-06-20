namespace ChatTalk.WebServer.Data.Dapper.Sql
{
    public static class ChatMessageSql
    {
        public static readonly string INSERT_001 = """
            INSERT INTO CHAT_MESSAGE (
                MESSAGE_ID,
                MESSAGE_TYPE,
                SENDER_USER_ID,
                TARGET_USER_ID,
                CONTENT
            )
            VALUES (
                @MessageId,
                @MessageType,
                @SenderUserId,
                @TargetUserId,
                @Content
            );
            """;
    }
}

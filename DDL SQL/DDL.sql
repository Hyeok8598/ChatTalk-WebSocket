-- DDL
-- CHAT_MESSAGE
CREATE TABLE chat_message (
	id BIGSERIAL PRIMARY KEY,
	message_id VARCHAR(36) NOT NULL,
	message_type VARCHAR(20) NOT NULL,
	sender_user_id VARCHAR(50) NOT NULL,
	target_user_id VARCHAR(50),
	content TEXT NOT NULL,
	create_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

commit;
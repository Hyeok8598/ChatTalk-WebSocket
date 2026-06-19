package com.chattalk.auth.config;

import com.github.vertical_blank.sqlformatter.SqlFormatter;
import com.p6spy.engine.spy.appender.MessageFormattingStrategy;

/**
 * P6Spy SQL 로그 포맷터.
 *
 * <p>
 * 실행된 SQL을 보기 쉽도록 포맷팅하여 출력한다.
 * </p>
 */

public class PrettySqlFormat implements MessageFormattingStrategy {
    @Override
    public String formatMessage(
            int connectionId,
            String now,
            long elapsed,
            String category,
            String prepared,
            String sql,
            String url) {

        if(sql == null || sql.trim().isEmpty()) {
            return "";
        }

        return "\n" + SqlFormatter.format(sql);
    }
}

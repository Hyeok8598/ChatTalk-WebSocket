package com.chattalk.auth.common.util;

import com.chattalk.auth.common.DataMap;
import com.fasterxml.jackson.databind.ObjectMapper;

public final class JsonUtil {
    private static final ObjectMapper OBJECT_MAPPER = new ObjectMapper();
    public static DataMap toDataMap(String json) {
        try {
            return OBJECT_MAPPER.readValue(
                    json,
                    DataMap.class
            );
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }
}

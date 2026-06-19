package com.chattalk.auth.common.util;

import java.util.Collection;
import java.util.Map;

/**
 * 공통 유틸리티 클래스.
 *
 * <p>
 * 프로젝트 전반에서 사용하는 공통 기능을 제공한다.
 * </p>
 *
 */

public final class CommonUtil {
    private CommonUtil() {}

    public static boolean isEmpty(Object value) {
        if(value == null) return true;

        if(value instanceof String str) return str.isBlank();

        if(value instanceof Collection<?> collection) return collection.isEmpty();

        if(value instanceof Map<?,?> map) return map.isEmpty();

        return false;
    }
}

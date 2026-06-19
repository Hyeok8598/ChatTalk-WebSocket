package com.chattalk.auth.common;

import java.util.*;

/**
 * 공통 데이터 컨테이너.
 *
 * <p>
 * DTO를 최소화하기 위해 사용하는 Key-Value 기반 객체이다.
 * 단건 데이터, 중첩 객체, 그리드(List<DataMap>)를 모두 저장할 수 있다.
 * </p>
 *
 * <p>
 * 타입 검증은 수행하지 않는다.
 * 호출부에서 올바른 타입으로 사용해야 한다.
 * </p>
 */

public class DataMap extends LinkedHashMap<String, Object> {
    public void setParam(String key, Object value) {
        super.put(key, value);
    }

    @SuppressWarnings("unchecked")
    public <T> T getParam(String key) {
        return (T) super.get(key);
    }
}

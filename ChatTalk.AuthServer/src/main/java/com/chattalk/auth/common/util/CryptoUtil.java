package com.chattalk.auth.common.util;

import org.mindrot.jbcrypt.BCrypt;

/**
 * 암호화 관련 유틸리티 클래스.
 *
 * <p>
 * BCrypt 기반의 암호화 및 검증 기능을 제공한다.
 * </p>
 */

public final class CryptoUtil {
    private CryptoUtil() {}

    public static String encode(String value) {
        return BCrypt.hashpw(
                value,
                BCrypt.gensalt()
        );
    }

    public static boolean matches(String rawValue, String encodedValue) {
        return BCrypt.checkpw(
                rawValue,
                encodedValue
        );
    }
}

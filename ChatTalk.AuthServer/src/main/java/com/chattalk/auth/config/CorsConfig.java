package com.chattalk.auth.config;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.config.annotation.CorsRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurer;

import java.util.List;

/**
 * CORS(Cross-Origin Resource Sharing) 설정 클래스.
 *
 * <p>
 * Client와 AuthServer 간의 Cross-Origin 요청을 허용하기 위한 설정이다.
 * application.yml 혹은 application.properties에 정의된 허용 Origin 정보를 읽어
 * Spring MVC 전역에 적용한다.
 * </p>
 */
@Configuration
public class CorsConfig {

    /*// 1. [application.yml] 파일 적용
    @Value("${cors.allowed-origins}")
    private List<String> allowedOrigins

    @Bean
    public WebMvcConfigurer corsConfigurer() {
        return new WebMvcConfigurer() {
            @Override
            public void addCorsMappings(CorsRegistry registry) {
                registry.addMapping("/**")
                        .allowedOrigins(
                                allowedOrigins.toArray(new String[0])
                        )
                        .allowedMethods("*")
                        .allowedHeaders("*");
            }
        };
    }*/

    // 2. [application.properties] 파일 적용
    @Value("${cors.allowed-origins}")
    private String[] allowedOrigins;

    @Bean
    public WebMvcConfigurer corsConfigurer() {
        return new WebMvcConfigurer() {
            @Override
            public void addCorsMappings(CorsRegistry registry) {
                registry.addMapping("/**")
                        .allowedOrigins(allowedOrigins)
                        .allowedMethods("*")
                        .allowedHeaders("*");
            }
        };
    }
}

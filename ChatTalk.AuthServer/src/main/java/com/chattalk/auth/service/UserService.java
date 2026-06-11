package com.chattalk.auth.service;

import com.chattalk.auth.dto.request.LoginRequest;
import com.chattalk.auth.dto.request.SignUpRequest;
import com.chattalk.auth.dto.response.LoginResponse;
import com.chattalk.auth.entity.UserEntity;
import com.chattalk.auth.repository.UserRepository;
import org.springframework.stereotype.Service;

@Service
public class UserService {

    private final UserRepository userRepository;

    public UserService(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    public void signUp(SignUpRequest request) {
        UserEntity userEntity = new UserEntity();

        userEntity.setUserId(request.getUserId());
        userEntity.setPassword(request.getPassword());
        userEntity.setUserName(request.getUserName());

        userRepository.save(userEntity);
    }

    public LoginResponse login(LoginRequest request) {
        UserEntity userEntity = userRepository.findByUserId(request.getUserId())
                .orElseThrow(() -> new IllegalArgumentException("존재하지 않는 사용자입니다."));

        if(!userEntity.getPassword().equals(request.getPassword())) {
            throw new IllegalArgumentException("비밀번호가 일치하지 않습니다.");
        }

        System.out.println("로그 : " + userEntity.getUserId() + userEntity.getPassword() + userEntity.getUserName());

        return new LoginResponse(
                userEntity.getId(),
                userEntity.getUserId(),
                userEntity.getUserName()
        );
    }
}

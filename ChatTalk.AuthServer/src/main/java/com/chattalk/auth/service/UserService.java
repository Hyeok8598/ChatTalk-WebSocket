package com.chattalk.auth.service;

import com.chattalk.auth.dto.SignUpRequest;
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
}

package com.chattalk.auth.legacy.repository;

import com.chattalk.auth.legacy.entity.UserEntity;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface UserRepository extends JpaRepository<UserEntity, Long> {
    Optional<UserEntity> findByUserId(String UserId);
}

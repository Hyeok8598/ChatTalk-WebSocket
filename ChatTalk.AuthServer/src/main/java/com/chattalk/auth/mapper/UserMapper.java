package com.chattalk.auth.mapper;

import com.chattalk.auth.mapper.dto.Insert001InDto;
import com.chattalk.auth.mapper.dto.SelectOne001InDto;
import com.chattalk.auth.mapper.dto.SelectOne001OutDto;
import com.chattalk.auth.mapper.dto.Update001InDto;
import org.apache.ibatis.annotations.Mapper;

@Mapper
public interface UserMapper {
    SelectOne001OutDto selectOne001(SelectOne001InDto inDto);
    int insert001(Insert001InDto inDto);
    int update001(Update001InDto inDto);
}
package com.chattalk.auth.mapper;

import com.chattalk.auth.common.DataMap;
import org.apache.ibatis.annotations.Mapper;

@Mapper
public interface UsersMapper {
    DataMap usersSelectOne001(DataMap dataMap);
    DataMap usersSelectOne002(DataMap dataMap);
    int usersInsert001(DataMap dataMap);
    int usersUpdate001(DataMap dataMap);
}
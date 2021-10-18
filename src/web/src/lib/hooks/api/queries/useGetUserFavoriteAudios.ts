import { useCallback } from "react";
import {
  useInfinitePagination,
  UseInfinitePaginationOptions,
  UseInfinitePaginationReturnType,
} from "~/lib/hooks";
import request from "~/lib/http";
import { AudioView, OffsetPagedList } from "~/lib/types";
import { GET_USER_FAVORITE_AUDIOS_QUERY_KEY } from "~/lib/hooks/api/keys";

type UseGetUserFavoriteAudiosParams = {
  size?: number;
};

export function useGetUserFavoriteAudios(
  username: string,
  params: UseGetUserFavoriteAudiosParams = {},
  options: UseInfinitePaginationOptions<AudioView> = {}
): UseInfinitePaginationReturnType<AudioView> {
  const fetcher = useCallback(
    async (offset: number) => {
      const { data } = await request<OffsetPagedList<AudioView>>({
        method: "get",
        url: `users/${username}/favorite/audios`,
        params: {
          ...params,
          offset,
        },
      });
      return data;
    },
    [username, params]
  );

  return useInfinitePagination(
    GET_USER_FAVORITE_AUDIOS_QUERY_KEY(username),
    fetcher,
    {
      ...options,
      enabled: !!username && (options.enabled ?? true),
    }
  );
}
